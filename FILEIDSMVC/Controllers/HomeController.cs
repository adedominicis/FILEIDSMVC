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
        public ActionResult Index()
        {
            return View();
        }



    }

}