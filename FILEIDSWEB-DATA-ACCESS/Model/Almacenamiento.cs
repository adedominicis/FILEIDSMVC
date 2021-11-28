using System;
using System.Web;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Almacenamiento
    {
        public Almacenamiento(string ruta)
        {
            RutaRaiz = ruta;
            FechaCreacion = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff");
        }
        public Almacenamiento()
        {
            FechaCreacion = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fff");
        }
        public int IdAlmacenamiento { get; set; }
        public string RutaRaiz { get; set; }
        public int VersionArchivo { get; set; }
        public string Revision { get; set; }
        public string Extension { get; set; }
        public string MD5 { get; set; }
        public Archivo Archivo { get; set; }
        public Metadata Metadata { get; set; }
        public string FechaCreacion { get; private set; }

        /// <summary>
        /// Archivo físico subido por el usuario
        /// </summary>
        public HttpPostedFileBase ArchivoFisico { get; set; }

        #region Métodos

        public string getLocalStoragePath()
        {
            return string.Format("{0}\\{1}.{2}", RutaRaiz, FechaCreacion, Extension);
        }

        #endregion

    }
}
