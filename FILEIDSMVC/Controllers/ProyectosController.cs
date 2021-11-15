using FILEIDSMVC.Models;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSWEB_DATA_ACCESS.FileManagement;
using FILEIDSWEB_DATA_ACCESS.Model;
using System;
using System.Collections.Generic;
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
        /// Inicia la vista de carga de archivos.
        /// </summary>
        /// <param name="pvm"></param>
        /// <returns></returns>
        public ActionResult RegistrarArchivo(ProyectosViewModel pvm)
        {
            RegistrarArchivoViewModel ravm = new RegistrarArchivoViewModel();

            ravm.NombreDirectorio = pvm.NombreSubDirectorioActivo;
            ravm.IdCarpetaPadre = pvm.IdSubDirectorioActivo;

            return View("RegistrarArchivo", ravm);
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
        /// Crear un nuevo proyecto.
        /// </summary>
        /// <returns></returns>

        public ActionResult CrearProyecto(ProyectosViewModel pvm)
        {
            ViewBag.Title = "Crear un nuevo proyecto";
            try
            {
                if (ModelState.IsValid)
                {
                    Directorio dir = new Directorio()
                    {
                        NombreDirectorio = pvm.NombreDirectorioRaizNuevo,
                        DescriptorDirectorio = pvm.DescriptorDirectorioRaizNuevo
                    };
                    dao.singleReturnQuery(q.CrearDirectorioRaiz(dir));
                    return View("Proyectos");
                }
                else
                {
                    return View(pvm);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessages = ex.Message;
                return View(new ProyectosViewModel());
            }
        }



        /// <summary>
        /// Eliminar proyecto.
        /// </summary>
        /// <param name = "proyecto" ></ param > 0
        ///

        //public ActionResult Eliminar(ProyectosModel proyecto)
        //{
        //    dao.singleReturnQuery(q.DesactivarDirectorioRaiz(proyecto.IdDirectorio));
        //    proyecto.ListaDirectorios = dao.ListarDirectorioRaiz();
        //    return View("Proyectos", proyecto);
        //}


        ///// <summary>
        ///// Editar
        ///// </summary>
        ///// <param name="proyecto"></param>
        ///// <returns></returns>
        //public ActionResult Editar(ProyectosModel proyecto)
        //{
        //    ViewBag.Title = string.Format("Editando: /{0}",proyecto.NombreDirectorio);
        //    return View("CrearProyecto",proyecto);
        //}


        ///// <summary>
        ///// Carga inicial
        ///// Menú de registro y carga de nuevos archivos.
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult RegistrarArchivo(ProyectosModel proyecto)
        //{

        //    //Model para pasar a la vista.
        //    ProyectosViewModel regArchivo = new ProyectosViewModel();
        //    regArchivo.NombreDirectorio = proyecto.NombreDirectorio;
        //    regArchivo.IdCarpetaPadre = proyecto.IdDirectorio;
        //    return View(regArchivo);
        //}

        ///// <summary>
        ///// Respuesta POST
        ///// Menú de registro y carga de nuevos archivos.
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult RegistrarArchivo(ProyectosViewModel model)
        //{
        //    ViewBag.ErrorMessages = "";
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (registerAndTransfer(model))
        //            {
        //                return View("ViewFile");
        //            }
        //            else
        //            {
        //                ViewBag.ErrorMessages = "Error al registrar o guardar el archivo";
        //            }
        //        }
        //        return View(new ProyectosViewModel());
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessages = ex.Message;
        //        return View(new ProyectosViewModel());
        //    }

        //}


        //#region Helpers
        //private bool registerAndTransfer(ProyectosViewModel model)
        //{
        //    //Si el archivo tiene algo
        //    if (model.ArchivoSubido.ContentLength > 0)
        //    {

        //        //Asignar nombre de archivo
        //        model.NombreArchivoSubido = Path.GetFileNameWithoutExtension(model.ArchivoSubido.FileName);

        //        //Si el usuario decide omitir el nombre del archivo, se usa el nombre del archivo subido.
        //        if (string.IsNullOrEmpty(model.NombreArchivo))
        //        {
        //            model.NombreArchivo = model.NombreArchivoSubido;
        //        }

        //        //Clases de manipulacion de archivos.
        //        FileManager fm = new FileManager();
        //        Metadata fData = new Metadata();
        //        //Archivo y ArchivoViewModel deberían venir de la misma interfaz o heredar de un comun.
        //        fData.NombreArchivo = model.NombreArchivo;
        //        fData.DescriptorEn = model.DescriptorEn;
        //        fData.DescriptorEs = model.DescriptorEs;
        //        fData.Oemsku = model.OemSku;
        //        fData.IdProyecto = Convert.ToInt32(model.IdCarpetaPadre);
        //        fData.DescriptorExtra = model.DescriptorExtra;
        //        fData.Extension = Path.GetExtension(model.ArchivoSubido.FileName);
        //        //Si la extensión no tiene un ID, quiza sea razonable que se agregue sola, como en Hormesa FILEIDS
        //        //Por ahora subamos solo archivos conocidos.

        //        fData.IdExtension = Convert.ToInt32(dao.singleReturnQuery(q.consultaIdFromExtension(fData.Extension)));

        //        //Agregar archivo.
        //        fData = dao.addRecord(fData);

        //        if (!string.IsNullOrEmpty(fData.Id))
        //        {
        //            fm.CrearArchivo(fData, model.ArchivoSubido);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //#endregion
    }
}