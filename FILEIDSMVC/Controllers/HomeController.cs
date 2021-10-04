using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSMVC.Models;
using System.IO;
using FILEIDSWEB_DATA_ACCESS.FileManagement;
using FILEIDSWEB_DATA_ACCESS.Model;
using System.Data;

namespace FILEIDSMVC.Controllers
{
    public class HomeController : Controller
    {
        //Acceso a datos.
        DAO dao = new DAO();
        queryDump q = new queryDump();
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Menú de registro y carga de nuevos archivos.
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistrarArchivo()
        {
            ViewBag.Message = "Registro de archivos";

            //Model para pasar a la vista.
            RegistroArchivoModel regArchivo = new RegistroArchivoModel(dao.getComboBoxData(q.viewNombresProyectos));

            return View(regArchivo);
        }

        /// <summary>
        /// Menú de registro y carga de nuevos archivos.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegistrarArchivo(RegistroArchivoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Si el archivo tiene algo
                    if (model.Archivo.ContentLength > 0)
                    {
                        //Asignar nombre de archivo
                        model.NombreArchivo = Path.GetFileNameWithoutExtension(model.Archivo.FileName);

                        //Clases de manipulacion de archivos.
                        FileManager fm = new FileManager();
                        FileMetaData fData = new FileMetaData();
                        //FileMetaData y RegistroArchivoModel deberían venir de la misma interfaz o heredar de un comun.
                        fData.DescriptorEn = model.DescriptorEn;
                        fData.DescriptorEs = model.DescriptorEs;
                        fData.Oemsku = model.OemSku;
                        fData.IdProyecto = Convert.ToInt32(model.IdProyecto);
                        fData.DescriptorExtra = model.DescriptorExtra;
                        fData.Extension = Path.GetExtension(model.Archivo.FileName);
                        //Si la extensión no tiene un ID, quiza sea razonable que se agregue sola, como en Hormesa FILEIDS
                        //Por ahora subamos solo archivos conocidos.
                        
                        fData.IdExtension = Convert.ToInt32(dao.singleReturnQuery(q.consultaIdFromExtension(fData.Extension)));

                        //Agregar archivo.
                        fData = dao.addRecord(fData);
                        
                        if (!string.IsNullOrEmpty(fData.Id))
                        {
                            fm.guardarArchivo(fData, model.Archivo);
                            return View("Index");
                        }
                    }
                }
                return View(new RegistroArchivoModel(dao.getComboBoxData(q.viewNombresProyectos)));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessages = ex.Message;
                return View(new RegistroArchivoModel(dao.getComboBoxData(q.viewNombresProyectos)));
            }

        }


    }
}