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
        public string CrearDirectorioRaiz(Directorio proyecto)
        {
            return string.Format("exec CrearDirectorioRaiz '{0}','{1}'", proyecto.NombreDirectorio, proyecto.DescriptorDirectorio);
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

        /// <summary>
        /// Crear nuevo archivo en la DB y el storage
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        public string CrearArchivo(Almacenamiento alm)
        {
            return string.Format("exec CrearArchivo '{0}',{1},'{2}','{3}'",
                alm.Archivo.NombreArchivo,
                alm.Archivo.IdDirectorioPadre,
                alm.Extension,
                alm.RutaAlmacenamiento);
        }

        /// <summary>
        /// Desarrollar recursivamente todas las carpetas y subcarpetas de un directorio particular
        /// </summary>
        /// <param name="idDirectorio"></param>
        /// <returns></returns>
        public string DesarrollarDirectorioRecursivo(int idDirectorio)
        {
            return string.Format("exec DesarrollarDirectorioRecursivo {0}", idDirectorio);
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

        internal string ListarArchivosSubDirectorio(object idDirectorio)
        {
            return string.Format("exec ListarArchivosSubDirectorio {0}", idDirectorio);
        }





        #endregion
    }
}