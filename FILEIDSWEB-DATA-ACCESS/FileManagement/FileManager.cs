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

        Ruta direccion = new Ruta();
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

        public bool guardarArchivo(FileMetaData fData, HttpPostedFileBase file)
        {
            // Guardar archivo en el directorio local con el nombre que le corresponde
            //Construir objeto ruta.
            rootDir = ConfigurationManager.AppSettings["FileCachePath"].ToString();
            Ruta ruta = new Ruta();
            ruta.IdArchivo = fData.Id;
            ruta.RevLevel = "1"; // Reemplazar por versioncontrol
            ruta.RevLetter = dao.singleReturnQuery(q.execGetRevisionLevelFromId(ruta.RevLevel));
            ruta.MD5 = "NONE"; // Funcionalidad MD5 viene mas adelante para control de cambios.

            try
            {
                // Construido el objeto Ruta, recibir el archivo

                //Si no existe el directorio raiz, crearlo.
                if (!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }

                // Path completo del archivo a guardar.
                ruta.StrRuta = string.Format(@"{0}{1}-{2}{3}", rootDir, fData.getNombreArchivo(), ruta.RevLetter, Path.GetExtension(file.FileName));
                
                //Guardar archivo. Es necesario añadir una verificacion
                file.SaveAs(ruta.StrRuta);
                //Registrar archivo en la base de datos.
                if (File.Exists(ruta.StrRuta))
                {
                    //El archivo fue guardado efectivamente y puede registrarse en la db
                    return registrarRuta(ruta);
                }
                else
                {
                    //El archivo no pudo registrarse en la db. Borrar fisico.
                    File.Delete(ruta.StrRuta);
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }
        private bool registrarRuta(Ruta ruta)
        {
            // Registrar en la base de datos que el archivo fue guardado en la ruta correspondiente.
            return dao.genericSelectQuery(q.execCargarRuta(ruta)) != null;
        }

        public bool eliminarArchivo(Ruta ruta)
        {
            if (dao.genericDeleteQuery(q.execBorrarRuta(ruta)))
            {
                if (File.Exists(ruta.StrRuta))
                {
                    File.Delete(ruta.StrRuta);
                }

                return true;
            }
            else
            {
                return false;
            }

        }

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
