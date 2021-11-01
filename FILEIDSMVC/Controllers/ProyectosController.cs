using FILEIDSMVC.Models;
using FILEIDSWEB_DATA_ACCESS;
using System;
using System.Collections.Generic;
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

        // GET: Explorer
        public ActionResult Proyectos()
        {
            ProyectosModel pm = new ProyectosModel();
            pm.ListaDirectorios= dao.getListaProyectos();
            return View(pm);
        }

        /// <summary>
        /// Crear un nuevo proyecto.
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearProyecto()
        {
            return View();
        }

        /// <summary>
        /// POST, crear proyecto
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        [HttpPost]
        public void CrearProyecto(ProyectosModel proyecto)
        {
            string queryReturn;
            ViewBag.ErrorMessages = "";
            try
            {
                if (ModelState.IsValid)
                {
                    queryReturn=dao.singleReturnQuery(q.CrearDirectorioRaiz(proyecto));
                }
                Proyectos();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessages = ex.Message;
                Proyectos();
            }
        }

        public void Detalles(ProyectosModel proyecto)
        {
            
            Proyectos();
        }
    }
}