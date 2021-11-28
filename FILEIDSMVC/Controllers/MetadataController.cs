using FILEIDSMVC.DataTransferFunctions;
using FILEIDSWEB_DATA_ACCESS;
using FILEIDSWEB_DATA_ACCESS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Controllers
{
    public class MetadataController : Controller
    {

        DAO dao = new DAO();
        queryDump q = new queryDump();

        // GET: Metadata
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdArchivo"></param>
        /// <param name="VersionArchivo"></param>
        /// <returns></returns>

        [HttpPost]
        public string GetMetadata(int IdArchivo,int VersionArchivo)
        {
            List<Metadata> Propiedades = new List<Metadata>();
            DataTable DTPropiedades=dao.genericSelectQuery(q.GetMetadata(IdArchivo,VersionArchivo));
            try
            {
                if (DTPropiedades.Rows.Count > 0)
                {
                    foreach (DataRow fila in DTPropiedades.Rows)
                    {
                        Propiedades.Add(new Metadata
                        {
                            IdMetadata = Convert.ToInt32(fila[0]),
                            Version = Convert.ToInt32(fila[1]),
                            DescriptorEs = fila[3].ToString(),
                            DescriptorEn = fila[4].ToString(),
                            Oemsku = fila[5].ToString(),
                            DescriptorExtra = fila[6].ToString()
                        });
                    }
                }
                
                return JsonConvert.SerializeObject(Propiedades).ToString();
            }
            catch (Exception ex)
            {
                return "Excepcion";
            }
           
        }

        /// <summary>
        /// Obtener un listado simple con las versiones existentes en el sistema.
        /// </summary>
        /// <param name="IdArchivo"></param>
        /// <returns></returns>
        public string ListarNumerosVersiones(int IdArchivo)
        {
            List<int> Versiones = new List<int>();
            DataTable DTVersiones = dao.genericSelectQuery(q.ListarNumerosVersiones(IdArchivo));
            try
            {
                if (DTVersiones.Rows.Count > 0)
                {
                    foreach (DataRow fila in DTVersiones.Rows)
                    {
                        Versiones.Add(Convert.ToInt32(fila[0]));
                    }
                }

                return JsonConvert.SerializeObject(Versiones).ToString();
            }
            catch (Exception ex)
            {
                return "Excepcion";
            }
        }

        /// <summary>
        /// Actualizar Metadata.
        /// </summary>
        /// <param name="IdArchivo"></param>
        /// <param name="DescriptorEs"></param>
        /// <param name="DescriptorEn"></param>
        /// <param name="DescriptorExtra"></param>
        /// <param name="OemSku"></param>
        /// <returns></returns>
        public string ActualizarMetadata(int IdArchivo,string DescriptorEs, string DescriptorEn, string DescriptorExtra, string OemSku)
        {
            Almacenamiento alm = DTO.AjaxMetadata_AlmacenamientoDTO(IdArchivo, DescriptorEs, DescriptorEn, DescriptorExtra, OemSku);
            string response= dao.singleReturnQuery(q.ActualizarMetadata(alm));
            return response;
        }
    }
}