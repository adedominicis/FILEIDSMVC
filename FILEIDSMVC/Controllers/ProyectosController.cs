using FILEIDSMVC.Models;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSWEB_DATA_ACCESS.Model;
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

        /// <summary>
        /// Pantalla principal de proyectos
        /// </summary>
        /// <returns></returns>
        public ActionResult Proyectos()
        {
            ProyectosModel pm = new ProyectosModel();
            pm.ListaDirectorios= dao.ListarDirectorioRaiz();
            return View("Proyectos",pm);
        }

        /// <summary>
        /// Crear un nuevo proyecto.
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearProyecto()
        {
            ViewBag.Title = "Crear un nuevo proyecto";
            return View();
        }

        /// <summary>
        /// POST, crear proyecto
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult CrearProyecto(ProyectosModel proyecto)
        {
            string queryReturn;
            ViewBag.ErrorMessages = "";
            try
            {
                if (ModelState.IsValid)
                {
                    queryReturn=dao.singleReturnQuery(q.CrearDirectorioRaiz(new Proyecto() {
                        NombreDirectorio=proyecto.NombreDirectorio,
                        DescriptorDirectorio=proyecto.DescriptorDirectorio
                    }));
                }
                proyecto.ListaDirectorios = dao.ListarDirectorioRaiz();
                return View("Proyectos", proyecto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessages = ex.Message;
                return View("Proyectos", proyecto);
            }

        }

        /// <summary>
        /// Detalles del proyecto, esto debe redirigir a la interfaz en donde se agregan nuevas carpetas y archivos.
        /// </summary>
        /// <param name="proyecto"></param>
        public ActionResult DetallesProyecto(Proyecto proyecto)
        {
            
            return View(new ProyectosModel(){ IdDirectorio=proyecto.IdDirectorio,NombreDirectorio=proyecto.NombreDirectorio});
        }


        /// <summary>
        /// Eliminar proyecto.
        /// </summary>
        /// <param name="proyecto"></param>0
        /// 

        public ActionResult Eliminar(ProyectosModel proyecto)
        {
            dao.singleReturnQuery(q.DesactivarDirectorioRaiz(proyecto.IdDirectorio));
            proyecto.ListaDirectorios = dao.ListarDirectorioRaiz();
            return View("Proyectos", proyecto);
        }


        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        public ActionResult Editar(ProyectosModel proyecto)
        {
            ViewBag.Title = string.Format("Editando: /{0}",proyecto.NombreDirectorio);
            return View("CrearProyecto",proyecto);
        }
    }
}