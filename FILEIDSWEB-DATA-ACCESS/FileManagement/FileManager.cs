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
        Almacenamiento alm;

        #endregion

        #region public properties

        /// <summary>
        /// Propiedad de solo lectura que expone el almacenamiento para ser usado por DTO's en el front end.
        /// </summary>
        public Almacenamiento miAlmacenamiento
        {
            get { return alm; }
        }

        #endregion

        #region constructors
        public FileManager(Almacenamiento almn)
        {
            alm = almn;
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
        /// <returns></returns>
        public bool CrearOActualizarRegistrosDeArchivos()
        {
            try
            {
                //Setear ruta de almacenamiento.
                alm.RutaRaiz = ConfigurationManager.AppSettings["FileCachePath"].ToString();

                //Guardar el archivo en el storage. Si tiene éxito, 

                if (GuardarFicheroEnStorage())
                {
                    alm.MD5 = GetMD5(alm.getLocalStoragePath());

                    //Verificación de referencias existentes.
                    int IdArchivoMD5Existente = Convert.ToInt32(dao.singleReturnQuery(q.VerificarMD5(alm.MD5)));
                    int IdArchivoNombreExistente = Convert.ToInt32(dao.singleReturnQuery(q.VerificarNombresDuplicados(alm)));

                    //No existe un MD5 en el storage ni archivo con el mismo nombre en la carpeta.
                    if (IdArchivoMD5Existente == 0 && IdArchivoNombreExistente == 0)
                    {
                        DataTable CrearArchivoResponse = dao.genericSelectQuery(q.CrearArchivo(alm));
                        if (CrearArchivoResponse != null)
                        {
                            if (CrearArchivoResponse.Columns.Count == 1)
                            {
                                CrearArchivoResponse.Rows[0].Field<string>("ERROR");
                                EliminarArchivoEnStorage();
                                return false;
                            }
                            else
                            {
                                alm.Archivo.IdArchivo = CrearArchivoResponse.Rows[0].Field<int>("ID_ARCHIVO");
                                alm.VersionArchivo = CrearArchivoResponse.Rows[0].Field<int>("VERSION_ARCHIVO");
                                alm.IdAlmacenamiento = CrearArchivoResponse.Rows[0].Field<int>("ID_ALMACENAMIENTO");
                                alm.RutaRaiz = CrearArchivoResponse.Rows[0].Field<string>("RUTA_ALMACENAMIENTO");
                                alm.Metadata.IdMetadata = CrearArchivoResponse.Rows[0].Field<int>("ID_METADATA");
                                if (InicializarMetadata())
                                {
                                    return true;
                                }
                                else
                                {
                                    EliminarArchivoEnStorage();
                                    return false;
                                }
                            }
                        }
                        return false;
                    }

                    //No existe un MD5 en el storage pero si existe un archivo con el mismo nombre
                    if (IdArchivoMD5Existente == 0 && IdArchivoNombreExistente != 0)
                    {
                        alm.Archivo.IdArchivo = IdArchivoNombreExistente;
                        if (ActualizarMetadata())
                        {
                            return true;
                        }
                        else
                        {
                            EliminarArchivoEnStorage();
                            return false;
                        }
                    }

                    //Existe un MD5 en el storage pero no existe un archivo con el mismo nombre
                    //[TODO] Aqui es posible que se requiera que el usuario intervenga con el nombre.
                    if (IdArchivoMD5Existente != 0 && IdArchivoNombreExistente == 0)
                    {
                        
                        alm.Archivo.IdArchivo = IdArchivoMD5Existente;
                        EliminarArchivoEnStorage();
                        return ActualizarMetadata();
                    }

                    //Existe un MD5 en el storage y un archivo con el mismo nombre
                    if (IdArchivoMD5Existente != 0 && IdArchivoNombreExistente != 0)
                    {
                        alm.Archivo.IdArchivo = IdArchivoNombreExistente;
                        EliminarArchivoEnStorage();
                        return ActualizarMetadata();
                    }
                    return false;
                }
                else
                {
                    EliminarArchivoEnStorage();
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region Interacción con DB
        /// <summary>
        /// Inicializar metadata de un archivo nuevo.
        /// Se entiende por archivo nuevo aquel que no tenia registros anteriores en la tabla ARCHIVOS.
        /// La tabla ARCHIVOS almacena la representación de la entidad fisica pero no sus detalles de versión o metadata.
        /// </summary>
        /// <returns></returns>
        private bool InicializarMetadata()
        {
            //select 1 as 'INFO' - Se actualizó la metadata en una nueva versión
            //select 2 as 'INFO' - Se actualizó la metadata sin crear nueva version
            //select 0 as 'INFO' - Error, transaccion no se ha llevado a cabo
            string queryResponse = dao.singleReturnQuery(q.InicializarMetadata(alm));
            return string.Equals(queryResponse, "2");
        }


        /// <summary>
        /// Actualizar metadata de un archivo existente.
        /// Se entiende por archivo nuevo aquel que no tenia registros anteriores en la tabla ARCHIVOS.
        /// La tabla ARCHIVOS almacena la representación de la entidad fisica pero no sus detalles de versión o metadata.
        /// </summary>
        /// <returns></returns>
        private bool ActualizarMetadata()
        {
            //select 1 as 'INFO' - Se actualizó la metadata en una nueva versión
            //select 2 as 'INFO' - Se actualizó la metadata sin crear nueva version
            //select 0 as 'INFO' - Error, transaccion no se ha llevado a cabo
            string queryResponse = dao.singleReturnQuery(q.ActualizarMetadata(alm));

            return string.Equals(queryResponse, "1");

        }







        #endregion


        #region Interacción con storage

        /// <summary>
        /// Inserta los archivos físicos en el sistema de almacenamiento del backend.
        /// </summary>
        /// <param name="alm"></param>
        /// <returns>True si pudo guardar archivo en local, false si no.</returns>
        private bool GuardarFicheroEnStorage()
        {
            try
            {
                //Si no existe el directorio raiz, crearlo.
                if (!Directory.Exists(alm.RutaRaiz))
                {
                    Directory.CreateDirectory(alm.RutaRaiz);
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
        /// Obtener el MD5 del archivo en un string
        /// </summary>
        /// <param name="path">Ruta absoluta del archivo</param>
        /// <returns></returns>
        private string GetMD5(string path)
        {
            byte[] MD5ByteArray;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    MD5ByteArray = md5.ComputeHash(stream);
                }
            }

            return BitConverter.ToString(MD5ByteArray).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Borra la copia física de un archivo en el sistema de almacenamiento del backend
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        private bool EliminarArchivoEnStorage()
        {
            try
            {
                //Si no existe el directorio raiz, crearlo.
                if (Directory.Exists(alm.RutaRaiz))
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
