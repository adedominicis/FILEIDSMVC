using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using FILEIDSWEB_DATA_ACCESS.Logging;
using FILEIDSWEB_DATA_ACCESS.Model;

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
                        conName = "localtest";
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

        // Obtener un objeto FileMetaData a partir de uno existente, refrescando desde la DB
        public FileMetaData getFileMetaDataFromDB(FileMetaData pb)
        {
            try
            {

                //Consulta
                string query = q.execGetFilePropertiesFromId(pb.Id);
                //Conectarse
                startConnection();

                //Dataset
                DataSet ds = new DataSet();
                //Data adapter
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                //Llenar dataset con el resultado de dataAdapter
                dataAdapter.Fill(ds, "properties");
                DataTable tbl = ds.Tables["properties"];
                // Datos de tabla ARCHIVOS y EXTENSIONES
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        pb.DescriptorEs = row[0].ToString();
                        pb.DescriptorEn = row[1].ToString();
                        pb.Oemsku = row[2].ToString();
                        pb.DescriptorExtra = row[3].ToString();
                        pb.IdExtension = int.Parse(row[5].ToString());
                    }
                }
                else
                {
                    pb.DescriptorEs = "No encontrado";
                    pb.DescriptorEn = "Not found";
                    pb.Oemsku = "";
                    pb.DescriptorExtra = "";
                    pb.IdExtension = -1;
                }

                // Datos de tablas entregables y proyectos
                query = q.execGetFileProjectAssociationFromId(pb.Id);
                dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.Fill(ds, "proyectos");
                tbl = ds.Tables["proyectos"];

                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        //pb.Proyecto= row[0].ToString();
                        pb.IdTipoEntregable = int.Parse(row[3].ToString());
                        pb.IdProyecto = int.Parse(row[4].ToString());
                    }
                }
                else
                {
                    pb.IdTipoEntregable = 0;
                    pb.IdProyecto = 0;
                }

            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.getFileMetaDataFromDB");
            }
            finally
            {
                connection.Close();
            }
            return pb;

        }

        // Agregar un nuevo registro en la DB desde un FileMetaData
        public FileMetaData addRecord(FileMetaData pb)
        {

            try
            {

                // Consulta

                string query = q.execInsertFileProperties(pb);

                // Verificar estado de la conexion
                startConnection();

                SqlCommand command = new SqlCommand(query, connection);
                //Ejecutar procedimiento para insertar en tabla Archivos y retornar ID
                string registeredID = command.ExecuteScalar().ToString();
                pb.Id = registeredID;

                //Ejecutar procedimiento para insertar en tabla Entregables.
                query = q.execInsertFileProjectAssociations(pb);
                command = new SqlCommand(query, connection);
                command.ExecuteScalar();
                return pb;
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.addRecord");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        // Actualizar un campo de la base de datos desde un FileMetaData
        public bool updateRecord(FileMetaData pb)
        {
            int rowsAffected = 0;

            try
            {
                //Consulta
                string query = q.execUpdateArchivos(pb);
                // Verificar estado de la conexion
                startConnection();

                SqlCommand command = new SqlCommand(query, connection);

                //Ejecutar comando
                rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                exceptionRaised?.Invoke(this, ErrorHandler.handler(EnumMensajes.errorSQL) + " " + conName + " " + ex.Message + "DAO.updateRecord");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        // Consulta generica "select" que retorna un solo string. 
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

        // Consulta generica "select" que retorna un datatable
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
                connection.Close();
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
        #endregion

        #region Queries especificos que deben ser actualizados a tipos más genéricos basados en querydump y procedimientos almacenados
        // Obtener todos los tipos de archivo para llenar combobox
        public ObservableCollection<string> getFileExtensions()
        {
            startConnection();
            ObservableCollection<string> listaTipos = new ObservableCollection<string>();
            DataTable extensiones = genericSelectQuery(q.viewNombresExtensiones);

            if (extensiones != null)
            {
                foreach (DataRow row in extensiones.Rows)
                {
                    listaTipos.Add(row[0].ToString());
                }

            }

            return listaTipos;
        }

        // Obtener proyectos disponibles para llenar combobox
        public ObservableCollection<string> getComboBoxData(string query)
        {
            startConnection();
            ObservableCollection<string> lista = new ObservableCollection<string>();
            DataTable dataTable = genericSelectQuery(query);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    lista.Add(row[0].ToString());
                }
            }
            return lista;
        }

        //Obtener el id de una extension de archivo particular.
        public string getFileIdFromExtension(string extension)
        {
            startConnection();
            DataTable res = genericSelectQuery(q.consultaIdFromExtension(extension));
            return res.Rows[0][0].ToString();
        }

        //Obtener la extension de un archivo desde la id
        public string getFileExtensionFromId(string id)
        {
            startConnection();
            DataTable res = genericSelectQuery(q.consultaExtensionFromId(id));
            return res.Rows[0][0].ToString();

        }

        //Obtener nombre del archivo desde id
        public string getFileNameFromId(string id)
        {
            startConnection();
            DataTable res = genericSelectQuery(q.execNombreArchivoDesdeId(id));
            if (res.Rows.Count > 0)
            {
                return res.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        //Obtener nombre del entregable desde id
        public string getDeliverableNameFromId(string id)
        {
            startConnection();
            DataTable res = genericSelectQuery(q.execNombreEntregableDesdeId(id));
            if (res.Rows.Count > 0)
            {
                return res.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        //Obtener nombre del proyecto desde id
        public string getProjectNameFromId(string id)
        {
            startConnection();
            DataTable res = genericSelectQuery(q.execProyectoDesdeId(id));
            if (res.Rows.Count > 0)
            {
                return res.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public Ruta obtenerRutaDesdeId(string id, string revisionLevel)
        {
            Ruta ruta = new Ruta();
            ruta.IdArchivo = id;
            ruta.RevLevel = revisionLevel;
            startConnection();

            DataTable res = genericSelectQuery(q.execGetRutaDesdeId(ruta));
            if (res.Rows.Count > 0)
            {
                ruta.StrRuta = res.Rows[0][1].ToString();
                ruta.MD5 = res.Rows[0][3].ToString();
            }
            return ruta;
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
    }


}

