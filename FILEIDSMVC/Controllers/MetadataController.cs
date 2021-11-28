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
                            DescriptorEs = fila[2].ToString(),
                            DescriptorEn = fila[3].ToString(),
                            Oemsku = fila[4].ToString(),
                            DescriptorExtra = fila[5].ToString()
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
    }
}