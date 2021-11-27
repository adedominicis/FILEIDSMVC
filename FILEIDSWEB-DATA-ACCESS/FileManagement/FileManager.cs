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
using System.Security.Cryptography;
using System.Data;

namespace FILEIDSWEB_DATA_ACCESS.FileManagement
{
    public class FileManager
    {

        // Directorios principales de sistema de archivos.
        #region private fields
        DAO dao = new DAO();
        queryDump q = new queryDump();
        #endregion

        #region public properties


        #endregion

        #region constructors
        public FileManager()
        {

        }

        #endregion

        #region Métodos públicos.
        /// <summary>
        /// Proceso completo de ingreso de archivos al sistema.
        /// 1- Carga de fichero en sistema de archivos
        /// 2- Si la carga del fichero resulta bien, cargar archivo en almacenamiento (DB)
        /// 3- Si el paso 2 resulta, termina el proceso.
        /// 4- Si el paso 2 no resulta, eliminar el fichero, termina el proceso.
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        public Almacenamiento IngresarFichero(Almacenamiento alm,out bool resIngresarFichero)
        {
            resIngresarFichero = false;
            //Setear ruta de almacenamiento.
            alm.RutaAlmacenamiento = ConfigurationManager.AppSettings["FileCachePath"].ToString();

            if (CargarFicheroEnLocal(alm))
            {
                alm = CrearActualizarAlmacenamiento(alm, out resIngresarFichero);
                if (!resIngresarFichero)
                {
                    EliminarArchivoEnLocal(alm);
                }
            }
            return alm;
        }

        /// <summary>
        /// Actualizar metadata de un archivo.
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        public bool ActualizarMetadata(Almacenamiento alm)
        {
            //Setear ruta de almacenamiento.
            alm.RutaAlmacenamiento = ConfigurationManager.AppSettings["FileCachePath"].ToString();

            dao.singleReturnQuery(q.InicializarMetadata(alm));
            return true;

        }

        #endregion





        #region Métodos privados

        /// <summary>
        /// Borra la copia física de un archivo en el sistema de almacenamiento del backend
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        private bool EliminarArchivoEnLocal(Almacenamiento alm)
        {
            try
            {
                //Si no existe el directorio raiz, crearlo.
                if (Directory.Exists(alm.RutaAlmacenamiento))
                {
                    File.Delete(alm.getLocalStoragePath());
                    return !File.Exists(alm.getLocalStoragePath());
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Inserta los archivos físicos en el sistema de almacenamiento del backend.
        /// </summary>
        /// <param name="alm"></param>
        /// <returns>True si pudo guardar archivo en local, false si no.</returns>
        private bool CargarFicheroEnLocal(Almacenamiento alm)
        {
            try
            {
                //Si no existe el directorio raiz, crearlo.
                if (!Directory.Exists(alm.RutaAlmacenamiento))
                {
                    Directory.CreateDirectory(alm.RutaAlmacenamiento);
                }

                //Guardar archivo físico
                alm.ArchivoFisico.SaveAs(alm.getLocalStoragePath());

                return File.Exists(alm.getLocalStoragePath());

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Controla la inserción de archivos en la base de datos.
        /// Este metodo es invocado cuando es seguro que el archivo físico fue almacenado en una caché
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns
        private Almacenamiento CrearActualizarAlmacenamiento(Almacenamiento alm,out bool TransactionResult)
        {
            TransactionResult = false;

            alm.MD5= GetMD5(alm.ArchivoFisico);

            //Id del almacenamiento donde se encuentra el MD5
            string IdAlmMd5Existente = dao.singleReturnQuery(q.VerificarMD5(alm.MD5));

            if (string.IsNullOrEmpty(IdAlmMd5Existente))
            {
                //El MD5 del archivo suministrado no existe en la DB
                int IdArchivoExistente = Convert.ToInt32(dao.singleReturnQuery(q.VerificarNombresDuplicados(alm)));

                if (IdArchivoExistente != 0)
                {
                    //Crear una nueva version del archivo con ID_ARCHIVO= IdArchivoExistente
                    
                    return alm;
                }
                else
                {
                    //Agregar archivo nuevo y actualizar propiedades desde la base de datos
                    DataTable CrearArchivoResponse=dao.genericSelectQuery(q.CrearArchivo(alm));
                    if (CrearArchivoResponse!=null)
                    {
                        alm.Archivo.IdArchivo = CrearArchivoResponse.Rows[0].Field<int>("ID_ARCHIVO");
                        alm.VersionArchivo = CrearArchivoResponse.Rows[0].Field<int>("VERSION_ARCHIVO");
                        alm.IdAlmacenamiento = CrearArchivoResponse.Rows[0].Field<int>("ID_ALMACENAMIENTO");
                        alm.RutaAlmacenamiento = CrearArchivoResponse.Rows[0].Field<string>("RUTA_ALMACENAMIENTO");
                        alm.Metadata.IdMetadata = CrearArchivoResponse.Rows[0].Field<string>("ID_METADATA");
                        TransactionResult = true;
                    }
                    return alm;

                }
            }
            else
            {
                //El MD5 del archivo suministrado existe en la DB. Funcionalidad pendiente.
                TransactionResult = false;
                return alm;
            }

        }

        /// <summary>
        /// Obtener el MD5 del archivo en un string
        /// </summary>
        /// <param name="file">Objeto archivo.</param>
        /// <returns></returns>
        private string GetMD5(HttpPostedFileBase file)
        {
            byte[] MD5ByteArray;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FileName))
                {
                    MD5ByteArray=md5.ComputeHash(stream);
                }
            }

            return BitConverter.ToString(MD5ByteArray).Replace("-", "").ToLowerInvariant();
        }

        #endregion

        #region Legacy y por corregir.
        public string verArchivo(string rutaRelativa)
        {

            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1200px\" height=\"600px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";

            return string.Format(embed, rutaRelativa);

        }


        #endregion
    }
}
