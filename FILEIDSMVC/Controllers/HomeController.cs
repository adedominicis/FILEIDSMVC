using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrarArchivo()
        {
            ViewBag.Message = "Registro de archivos";

            return View();
        }

        public ActionResult ListarArchivos()
        {
            ViewBag.Message = "Archivos registrados";

            return View();
        }
    }
}