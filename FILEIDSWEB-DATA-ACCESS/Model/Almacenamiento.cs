 using System.Web;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Almacenamiento
    {
        public Almacenamiento(string ruta)
        {
            RutaAlmacenamiento = ruta;
        }
        public Almacenamiento()
        {
        }
        public int IdAlmacenamiento { get; set; }
        public string RutaAlmacenamiento { get; set; }
        public int VersionArchivo { get; set; }
        public string Revision { get; set; }
        public string Extension { get; set; }
        public string MD5 { get; set; }
        public Archivo Archivo { get; set; }
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Archivo físico subido por el usuario
        /// </summary>
        public HttpPostedFileBase ArchivoFisico { get; set; }

        #region Métodos

        public string getLocalStoragePath()
        {
            return string.Format("{0}\\{1}.{2}",RutaAlmacenamiento,IdAlmacenamiento,Extension);
        }
        #endregion

    }
}
