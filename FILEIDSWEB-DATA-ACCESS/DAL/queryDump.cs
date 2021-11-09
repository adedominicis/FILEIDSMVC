using FILEIDSWEB_DATA_ACCESS.Model;
using System;

namespace FILEIDSWEB_DATA_ACCESS
{
    public class queryDump
    {

        #region DATABASE VIEWS

        public string viewPropiedadesArchivos { get { return "select * from viewPropiedadesArchivos order by ID desc"; } }

        public string viewNombresArchivos { get { return "select * from nombres_archivos order by 'nombre archivo'  desc"; } }

        public string viewNombresProyectos { get { return "select * from viewNombresProyectos "; } }

        public string viewNombresExtensiones { get { return "select * from viewNombresExtensiones"; } }

        public string viewNombresTiposEntregables { get { return "select * from viewNombresTiposEntregables"; } }

        public string viewNombresArchivosEntregables { get { return @"select*from archivos"; } }

        public string viewExtensiones { get { return @"select * from viewExtensiones"; } }

        #endregion

        #region Procedimientos Almacenados.

        /// <summary>
        /// Crear directorio raiz en la DB.
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        public string CrearDirectorioRaiz(Proyecto proyecto)
        {
            return string.Format("exec CrearDirectorioRaiz '{0}','{1}'",proyecto.NombreDirectorio,proyecto.DescriptorDirectorio );
        }

        /// <summary>
        /// Listar directorios raiz
        /// </summary>
        /// <returns></returns>
        public string ListarDirectorioRaiz()
        {
            return string.Format("exec ListarDirectorioRaiz");
        }

        /// <summary>
        /// Desactivar directorio raiz.
        /// </summary>
        /// <param name="idDirectorio"></param>
        /// <returns></returns>
        public string DesactivarDirectorioRaiz(int idDirectorio)
        {
            return string.Format("exec DesactivarDirectorioRaiz {0}", idDirectorio);
        }
        #endregion

        #region Legacy y no revisados
        public string execProyectoDesdeId(string id)
        {
            return @"exec getNombreProyecto '" + id + "'";
        }



        public string execNombreArchivoDesdeId(string id)
        {
            return @"exec getNombreArchivo '" + id + "'";
        }
        public string execNombreEntregableDesdeId(string id)
        {
            return @"exec getNombreEntregable '" + id + "'";
        }



        public string execGetBundleDesdeId(string id)
        {
            //En desuso, revisar.
            return @"exec getbundle '" + id + "'";
        }

        public string execGetFilePropertiesFromId(string id)
        {
            return @"exec getFilePropertiesFromId '" + id + "'";
        }

        public string execGetFileProjectAssociationFromId(string id)
        {
            return @"exec getFileProjectAssociationFromId '" + id + "'";
        }

        public string execInsertFileProperties(FileMetaData pb)
        {
            return @"exec InsertFileProperties '" +
                pb.DescriptorEs + "','" +
                pb.DescriptorEn + "','" +
                pb.Oemsku + "'," +
                pb.IdExtension + ",'" +
                pb.DescriptorExtra + "'";
        }


        public string execGetNombreEntregableDesdeId(string id)
        {
            return @"exec getNombreEntregable '" + id + "'";
        }

        public string execCargarRuta(Ruta ruta)
        {
            return @"exec cargarRuta '" + ruta.IdArchivo + "','" + ruta.StrRuta + "','" + ruta.RevLevel + "','" + ruta.MD5 + "'";
        }

        public string execGetRevisionLevelFromId(string id)
        {
            return @"exec GetRevisionLevelFromId '" + id + "'";
        }

        internal string execBorrarRuta(Ruta ruta)
        {
            return @"exec borrarRuta '" + ruta.IdArchivo + "','" + ruta.RevLevel + "'";
        }

        public string execGetRutaDesdeId(Ruta ruta)
        {
            return @"exec obtenerRutaDesdeId '" + ruta.IdArchivo + "','" + ruta.RevLevel + "'";
        }

        public string execGetArchivosLocalesLike(string keywords)
        {
            return @"exec buscarArchivosLocalesLike '" + keywords + "'";
        }


        #endregion

        #region Queries convencionales por cambiar a procedimiento almacenado


        //Convertir a procedimiento almacenado execIdFromExtension
        public string consultaIdFromExtension(string extension)
        {
            return @"select id from extensiones where extension like '%" + extension + "'";
        }

        //Convertir a procedimiento almacenado execExtensionFromId
        public string consultaExtensionFromId(string id)
        {
            return @"select extension from extensiones where id='" + id + "'";
        }

        //Convertir a procedimiento almacenado execConsultaBusqueda
        public string consultaPropiedadesLike(string keywords)
        {
            return @"exec buscarPropiedadesLike '" + keywords + "'";
        }

        //Convertir a procedimiento almacenado execCheckExistanceOnDb
        public string consultaCheckExistanceOnDb(string id)
        {
            return @"select count(*) id from archivos where id ='" + id + "'";
        }

        public string execLogException(string message)
        {
            return @"exec execLogExceptions '" + message + "'";
        }



        #endregion
    }
}