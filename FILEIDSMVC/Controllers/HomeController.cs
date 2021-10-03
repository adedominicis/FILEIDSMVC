using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSMVC.Models;

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
            RegistroArchivoModel regArchivo = new RegistroArchivoModel();

            //Precarga de datos en el model.
            regArchivo.setExtensiones(dao.getComboBoxData(q.viewNombresExtensiones));

            return View(regArchivo);
        }

        public ActionResult RegistrarArchivosDeux()
        {
            return View();
        }

        //
        // POST: 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DespuesRegistro(RegistroArchivoModel model)
        {
            string extension = model.IdExtension;
            model.setExtensiones(dao.getComboBoxData(q.viewNombresExtensiones));
            model.IdExtension = "4";
            return View("RegistrarArchivo",model);
        }
    }
}