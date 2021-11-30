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
using FILEIDSMVC.DataTransferFunctions;

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
            pvm.Archivos = dao.ListarArchivosSubDirectorio(pvm.IdSubDirectorioActivo);
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
                    return Proyectos();
                }
                else
                {
                    return View(cProyVm);
                }

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
        /// <param name="cSubDirVm"></param>
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
                IdDirectorioPadre = IdDirPadre
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
                    if (AccesoFileManager(ravm))
                    {
                        //Algun mensaje aqui de éxito aca.
                        return Proyectos();
                    }
                    return Proyectos();
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
        private bool AccesoFileManager(RegistrarArchivoViewModel ravm)
        {

            //Si el archivo no viene vacío.
            if (ravm.ArchivoSubido.ContentLength > 0)
            {
                //Asignar nombre de archivo y extensión SIN PUNTO.
                ravm.NombreArchivoSubido = Path.GetFileNameWithoutExtension(ravm.ArchivoSubido.FileName);
                ravm.Extension = Path.GetExtension(ravm.ArchivoSubido.FileName).Replace(".", "");
                
                //Si el usuario decide omitir el nombre del archivo, se usa el nombre del archivo subido.
                if (string.IsNullOrEmpty(ravm.NombreArchivo))
                {
                    ravm.NombreArchivo = ravm.NombreArchivoSubido;
                }

                //Iniciar proceso de ingreso de archivos.
                FileManager fm = new FileManager(DTO.RegArchivo_AlmacenamientoDTO(ravm));

                //Se usa una función de transferencia estática para traducir desde el view model ravm al modelo alm
                return fm.CrearOActualizarRegistrosDeArchivos();
            }
            return false;
        }

        #endregion


    }
}