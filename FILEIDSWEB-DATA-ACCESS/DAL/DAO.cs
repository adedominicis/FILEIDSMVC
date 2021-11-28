using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using FILEIDSWEB_DATA_ACCESS.Logging;
using FILEIDSWEB_DATA_ACCESS.Model;
using System.Collections.Generic;


namespace FILEIDSWEB_DATA_ACCESS
{
    public class DAO
    {

        // Data access object.
        private string conStr;
        private SqlConnection connection;
        private string conName;
        private queryDump q = new queryDump();
        public event EventHandler<string> exceptionRaised;

        #region Conexión a DB
        // Database index:  1:localtest,  2: azureproductionmirs

        private int dbIndex = 1;
        public DAO() { }
        public DAO(int dbindex)
        {
            dbIndex = dbindex;
        }

        public void startConnection()
        {
            try
            {
                switch (dbIndex)
                {
                    case 1:
                        conName = "FILEIDS-APP-LOCAL";
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
                conStr = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
                connection = new SqlConnection(conStr);
                connection.Open();
            }
            catch (Exception e)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorEnConexionDB) + conName + "\\n" + e.Message);
            }
        }



        // Get connection name
        public string getConnectionName()
        {
            return conName;
        }
        #endregion

        #region Core queries
        // revisar si un ID existe en la tabla de archivos
        public bool checkExistenceOnDb(string id)
        {
            try
            {

                //Consultar existencia del archivo
                string query = q.consultaCheckExistanceOnDb(id);

                startConnection();

                SqlCommand command = new SqlCommand(query, connection);

                //Ejecutar comando

                string existencia = command.ExecuteScalar().ToString();
                connection.Close();

                return !existencia.Equals("0");
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.checkExistanceOnDB");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Consulta genérica que retorna un solo resultado escalar en string.
        /// </summary>
        /// <param name="query">Consulta SQL.</param>
        /// <returns></returns>
        public string singleReturnQuery(string query)
        {
            try
            {
                // Check for an available connection
                startConnection();
                //Todo esto parece estar mal. Deberia usarse sqlcommand.executescalar. Revisar.
                //Dataset
                DataSet ds = new DataSet();
                //Data adapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                //Llenar dataset con el resultado de dataAdapter
                dataAdapter.Fill(ds, "resultado");
                DataTable tbl = ds.Tables["resultado"];
                return tbl.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.singleReturnQuery");
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

       /// <summary>
       /// Consulta genéricaque retorna un datatable.
       /// </summary>
       /// <param name="query"></param>
       /// <returns></returns>
        public DataTable genericSelectQuery(string query)
        {
            try
            {
                // Check for an available connection
                startConnection();
                //Dataset
                DataSet ds = new DataSet();
                //Data adapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                //Llenar dataset con el resultado de dataAdapter
                dataAdapter.Fill(ds, "genericSelectQuery");
                DataTable tbl = ds.Tables["genericSelectQuery"];
                ds = null;
                return tbl;
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.genericSelectQuery");
            }
            finally
            {
                if (connection!=null)
                {
                    connection.Close();
                }
                
            }
            return null;
        }

        // Este query retorna listas de strings listos para llenar comboboxes y similares. Debe reemplazar varios metodos redundantes mas abajo
        public ObservableCollection<string> observableCollectionQuery(string query)
        {
            startConnection();
            ObservableCollection<string> lista = new ObservableCollection<string>();
            DataTable tabla = genericSelectQuery(query);

            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    lista.Add(row[0].ToString());
                }
            }

            return lista;
        }

        public bool genericDeleteQuery(string query)
        {
            // Esto podria ser reemplazado por un singlereturnquery?|||
            try
            {
                // Check for an available connection
                startConnection();

                //Dataset
                DataSet ds = new DataSet();
                //Data adapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                //Llenar dataset con el resultado de dataAdapter
                dataAdapter.Fill(ds, "resultado");
                DataTable tbl = ds.Tables["resultado"];
                return true;
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.genericDeleteQuery");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// Query generico List<string>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string> genericListQuery(string query)
        {
            startConnection();
            List<string> lista = new List<string>();
            DataTable tabla = genericSelectQuery(query);

            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    lista.Add(row[0].ToString());
                }
            }

            return lista;
        }



        #endregion

        #region Consultas con retorno de objetos especificos.

        /// <summary>
        /// Obtener listado de directorios raiz (proyectos)
        /// </summary>
        /// <returns></returns>

        public List<Directorio> ListarDirectorioRaiz()
        {
            List<Directorio> lista = new List<Directorio>();
            startConnection();

            DataTable res = genericSelectQuery(q.ListarDirectorioRaiz());

            if (res.Rows.Count > 0)
            {
                foreach (DataRow fila in res.Rows)
                {
                    lista.Add(new Directorio() { 
                        IdDirectorio= Convert.ToInt32(fila[0]),
                        NombreDirectorio = fila[1].ToString(),
                        DescriptorDirectorio = fila[2].ToString(),
                        DirectorioActivo= Convert.ToBoolean(fila[3])
                    });
                }
            }
            return lista;
        }

        /// <summary>
        /// Listar sub directoriosm de un directorio raiz
        /// </summary>
        /// <param name="idDirectorio"></param>
        /// <returns></returns>
        public List<Directorio> ListarSubDirectorios(int idDirectorio)
        {
            List<Directorio> lista = new List<Directorio>();

            startConnection();

            DataTable res = genericSelectQuery(q.DesarrollarDirectorioRecursivo(idDirectorio));
            if (res.Rows.Count > 0)
            {
                foreach (DataRow fila in res.Rows)
                {

                    lista.Add(new Directorio()
                    {
                        IdDirectorio = Convert.ToInt32(fila[0]),
                        IdDirectorioPadre = ConvertirNullCero(fila[2].ToString()),
                        NombreDirectorio = fila[1].ToString(),
                        Profundidad = Convert.ToInt32(fila[3].ToString())

                    });

                    
                }
            }
            return lista;
        }

        /// <summary>
        /// Listado de archivos de un subdirectorio
        /// </summary>
        /// <param name="idSubDirectorio"></param>
        /// <returns></returns>
        public List<Archivo> ListarArchivosSubDirectorio(int idSubDirectorio)
        {
            List<Archivo> lista = new List<Archivo>();
            startConnection();

            DataTable res = genericSelectQuery(q.ListarArchivosSubDirectorio(idSubDirectorio));
            if (res.Rows.Count > 0)
            {
                foreach (DataRow fila in res.Rows)
                {

                    lista.Add(new Archivo()
                    {
                        Version= Convert.ToInt32(fila[0]),
                        IdArchivo = Convert.ToInt32(fila[1]),
                        NombreArchivo= fila[2].ToString(),
                        Extension = fila[3].ToString(),
                        Revision = fila[4].ToString(),
                        IdMetadata = Convert.ToInt32(fila[5])
                    });

                }
            }
            return lista;
        }
        #endregion

        #region Consultas Legacy


        // Obtener todos los tipos de archivo para llenar combobox
        public List<Extensiones> getFileExtensions()
        {
            startConnection();
            List<Extensiones> listaTipos = new List<Extensiones>();
            DataTable extensiones = genericSelectQuery(q.viewExtensiones);

            if (extensiones != null)
            {
                foreach (DataRow row in extensiones.Rows)
                {
                    listaTipos.Add(new Extensiones { IdExtension = Convert.ToInt32(row[0]), Extension = row[1].ToString() });
                }

            }

            return listaTipos;
        }

        /// <summary>
        /// Listado configurado para alimentar @Html.DropDownList en MVC.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<(string, string)> getComboBoxData(string query)
        {
            startConnection();
            List<(string,string)> lista = new List<(string, string)>();
            DataTable dataTable = genericSelectQuery(query);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ( string Value, string Text) rowTuple= (row[0].ToString(), row[1].ToString());
                    lista.Add(rowTuple);
                }
            }
            return lista;
        }


        public bool logException(string e)
        {

            try
            {
                // Consulta

                string query = q.execLogException(e);

                // Verificar estado de la conexion
                startConnection();

                SqlCommand command = new SqlCommand(query, connection);
                //Ejecutar procedimiento para insertar en tabla Archivos y retornar ID
                return command.ExecuteScalar().ToString() != null;
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.logException");
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        #endregion

        #region Helpers
        private int ConvertirNullCero(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(str);
            }
        }
        #endregion
    }

}

