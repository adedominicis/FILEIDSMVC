using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FILEIDSWEB_DATA_ACCESS.Model;
using FILEIDSWEB_DATA_ACCESS.Logging;
using System.Web;
using System.IO;
using System.Configuration;

namespace FILEIDSWEB_DATA_ACCESS.FileManagement
{
    public class FileManager
    {

        // Directorios principales de sistema de archivos.
        #region private fields

        Archivo direccion = new Archivo();
        DAO dao = new DAO();
        queryDump q = new queryDump();
        private string oemPath;
        private string productosPath;
        private string proyectosPath;
        private string rootDir;
        #endregion

        #region public properties
        public string OemPath
        {
            get { return oemPath; }
            set { oemPath = value; }
        }
        public string ProductosPath
        {
            get { return productosPath; }
            set { productosPath = value; }
        }
        public string ProyectosPath
        {
            get { return proyectosPath; }
            set { proyectosPath = value; }
        }
        public string CachePath
        {
            get { return rootDir; }
            set { rootDir = value; }
        }

        #endregion

        #region constructors
        public FileManager()
        {
  
        }

        public FileManager(string oempath, string productospath, string proyectospath, string cachepath)
        {
            oemPath = oempath;
            productosPath = productospath;
            proyectosPath = proyectospath;
            rootDir = cachepath;

        }
        #endregion

        // Administrador de archivos.

        public bool CrearArchivo(Almacenamiento alm)
        {
            //Setear ruta de almacenamiento.
            alm.RutaAlmacenamiento=ConfigurationManager.AppSettings["FileCachePath"].ToString();

            try
            {
                //Si no existe el directorio raiz, crearlo.
                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }

                //Guardar archivo físico
                alm.ArchivoFisico.SaveAs(alm.getLocalStoragePath());
                // Path completo del archivo a guardar.

                if (File.Exists(alm.getLocalStoragePath()))
                {
                    //El archivo fue guardado efectivamente y puede registrarse en la db
                    return dao.genericSelectQuery(q.CrearArchivo(alm)) != null;
                }
                else
                {
                    //El archivo no pudo registrarse en la db. Borrar fisico.
                    File.Delete(alm.getLocalStoragePath());
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }

        //public bool eliminarArchivo(Archivo ruta)
        //{
        //    if (dao.genericDeleteQuery(q.execBorrarRuta(ruta)))
        //    {
        //        if (File.Exists(ruta.StrRuta))
        //        {
        //            File.Delete(ruta.StrRuta);
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public string verArchivo(string rutaRelativa)
        {

            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1200px\" height=\"600px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";

            return string.Format(embed, rutaRelativa);

        }
    }
}
