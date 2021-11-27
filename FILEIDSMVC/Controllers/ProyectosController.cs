using FILEIDSMVC.Models;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSWEB_DATA_ACCESS.FileManagement;
using FILEIDSWEB_DATA_ACCESS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Controllers
{
    public class ProyectosController : Controller
    {

        //Acceso a datos.
        DAO dao = new DAO();
        queryDump q = new queryDump();

        #region Métodos
        /// <summary>
        /// Pantalla principal de proyectos, inicia la carga básica de atributos.
        /// </summary>
        /// <returns></returns>
        public ActionResult Proyectos()
        {
            ProyectosViewModel pvm = new ProyectosViewModel();
            pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
            return View("Proyectos", pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public ActionResult DetallesProyecto(ProyectosViewModel pvm)
        {
            pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
            pvm.SubDirectorios = dao.ListarSubDirectorios(pvm.IdDirectorioRaizActivo);
            return View("Proyectos", pvm);
        }



        /// <summary>
        /// Lista todos los archivos de un subdirectorio
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public ActionResult ListarArchivosSubdirectorio(ProyectosViewModel pvm)
        {
            pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
            pvm.SubDirectorios = dao.ListarSubDirectorios(pvm.IdDirectorioRaizActivo);
            pvm.Almacenamientos = dao.ListarArchivosSubDirectorio(pvm.IdSubDirectorioActivo);
            return View("Proyectos", pvm);
        }

        /// <summary>
        /// Crear un nuevo proyecto
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearProyecto()
        {
            ViewBag.Title = "Crear un nuevo proyecto";
            return View(new CrearProyectoViewModel());
        }

        /// <summary>
        /// Crear un nuevo proyecto. POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearProyecto(CrearProyectoViewModel cProyVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Directorio dir = new Directorio()
                    {
                        NombreDirectorio = cProyVm.NombreProyecto,
                        DescriptorDirectorio = cProyVm.DescriptorProyecto
                    };
                    dao.singleReturnQuery(q.CrearDirectorioRaiz(dir));

                    //[TODO] Debe haber una manera mas elegante de hacer esto.
                    //pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
                    //return View("Proyectos", pvm);
                }
                else
                {
                    return View(cProyVm);
                }

                return Proyectos();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessages = ex.Message;
                return View(new ProyectosViewModel());
            }
        }


        /// <summary>
        /// Crear subdirectorio
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public ActionResult CrearSubDirectorio(int IdDirPadre, int IdDirRaiz, string NombreDirPadre)
        {

            //Viewmodel para el formulario.
            CrearDirectorioViewModel cdvm = new CrearDirectorioViewModel()
            {
                NombreDirectorioPadre = NombreDirPadre,
                IdDirectorioPadre = IdDirPadre,
                IdDirectorioRaiz = IdDirRaiz
            };

            return View("CrearSubDirectorio", cdvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdDirPadre"></param>
        /// <param name="IdDirRaiz"></param>
        /// <param name="NombreDirPadre"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearSubDirectorio(CrearDirectorioViewModel cSubDirVm)
        {
            ProyectosViewModel pvm = new ProyectosViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    Directorio dir = new Directorio()
                    {
                        DescriptorDirectorio = cSubDirVm.DescriptorNuevoDirectorio,
                        IdDirectorioPadre = cSubDirVm.IdDirectorioPadre,
                        IdDirectorioRaiz = cSubDirVm.IdDirectorioRaiz,
                        NombreDirectorio = cSubDirVm.NombreNuevoDirectorio
                    };
                    dao.singleReturnQuery(q.CrearSubDirectorio(dir));

                    //Retornar a proyectos.
                    return Proyectos();

                }
                else
                {
                    return View(cSubDirVm);
                }
            }
            catch (Exception)
            {
                pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
                return View("Proyectos", pvm);
            }

        }

        /// <summary>
        /// Acción GET de la carga de archivos, inicia el menú
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public ActionResult RegistrarArchivo(int IdDirPadre, string NombreDirPadre)
        {

            RegistrarArchivoViewModel ravm = new RegistrarArchivoViewModel()
            {
                NombreDirectorio = NombreDirPadre,
                IdCarpetaPadre = IdDirPadre
            };

            return View("RegistrarArchivo", ravm);
        }

        /// <summary>
        /// Acción POST de registro de archivo.
        /// </summary>
        /// <param name="IdDirPadre"></param>
        /// <param name="NombreDirPadre"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrarArchivo(RegistrarArchivoViewModel ravm)
        {
            ProyectosViewModel pvm = new ProyectosViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    //Registrar archivo...
                    InitProcesoRegistro(ravm, out bool resProcesoRegistro);
                    
                    if (resProcesoRegistro)
                    {
                        return View("Proyectos", pvm);
                    }
                    return View("Proyectos", pvm);
                }
                else
                {
                    return View("RegistrarArchivo", ravm);
                }
            }
            catch (Exception)
            {
                pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
                return View("Proyectos", pvm);
            }
        }


        #endregion

        #region Comentados


        /// <summary>
        /// 
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        public ActionResult EliminarDirectorio(int Id)
        {
            dao.singleReturnQuery(q.DesactivarDirectorioRaiz(Id));
            ProyectosViewModel pvm = new ProyectosViewModel();
            pvm.DirectoriosRaiz = dao.ListarDirectorioRaiz();
            return View("Proyectos", pvm);

        }

        #endregion


        #region Helpers
        private Almacenamiento InitProcesoRegistro(RegistrarArchivoViewModel ravm,out bool resProcesoRegistro)
        {
            Almacenamiento alm = new Almacenamiento();
            resProcesoRegistro = false;

            //Si el archivo no viene vacío.
            if (ravm.ArchivoSubido.ContentLength > 0)
            {

                //Asignar nombre de archivo
                ravm.NombreArchivoSubido = Path.GetFileNameWithoutExtension(ravm.ArchivoSubido.FileName);

                //Si el usuario decide omitir el nombre del archivo, se usa el nombre del archivo subido.
                if (string.IsNullOrEmpty(ravm.NombreArchivo))
                {
                    ravm.NombreArchivo = ravm.NombreArchivoSubido;
                }

                //Iniciar proceso de ingreso de archivos.
                FileManager fm = new FileManager();
                alm = fm.IngresarFichero(AlmacenamientoDataAccessMapper(ravm), out bool resultado);
                if (resultado)
                {
                    //Fichero ingresado correctamente, ingresar metadata.
                    resProcesoRegistro= fm.ActualizarMetadata(alm);
                }
            }
            return alm;
        }

        /// <summary>
        /// Conversor del ViewModel al Data Access Model para la clase Almacenamiento del Data Access Model.
        /// </summary>
        /// <param name="ravm"></param>
        /// <returns></returns>
        private Almacenamiento AlmacenamientoDataAccessMapper(RegistrarArchivoViewModel ravm)
        {
            //Objetos de almacenamiento.
            Metadata met = new Metadata()
            {
                DescriptorEs = ravm.DescriptorEs,
                DescriptorEn = ravm.DescriptorEn,
                DescriptorExtra = ravm.DescriptorExtra,
                Oemsku=ravm.OemSku
            };
            Directorio dir = new Directorio()
            {
                NombreDirectorio = ravm.NombreDirectorio,
                IdDirectorioPadre = ravm.IdCarpetaPadre
            };

            Archivo arc = new Archivo()
            {
                IdDirectorioPadre = ravm.IdCarpetaPadre,
                NombreArchivo = ravm.NombreArchivo,
                DirectorioPadre = dir
            };
            Almacenamiento alm = new Almacenamiento(ConfigurationManager.AppSettings["FileCachePath"].ToString())
            {
                ArchivoFisico = ravm.ArchivoSubido,
                Extension = ravm.Extension,
                Archivo = arc,
                Metadata=met
                
            };

            return alm;
        }
        #endregion


    }
}