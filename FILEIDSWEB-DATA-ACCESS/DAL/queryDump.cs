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
        internal string ListarDirectorioRaiz()
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
        /// Inicializar la metadata de un archivo que se acaba de crear.
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        internal string InicializarMetadata(Almacenamiento alm)
        {
            return string.Format("exec ActualizarMetadata {0},{1},'{2}','{3}','{4}','{5}',{6}",
            alm.Archivo.IdArchivo,
            alm.VersionArchivo,
            alm.Metadata.DescriptorEs,
            alm.Metadata.DescriptorEn,
            alm.Metadata.Oemsku,
            alm.Metadata.DescriptorExtra,0);
        }

        /// <summary>
        /// Crear nuevo archivo en la DB y el storage
        /// </summary>
        /// <param name="alm"></param>
        /// <returns></returns>
        public string CrearArchivo(Almacenamiento alm)
        {
            return string.Format("exec CrearArchivo '{0}',{1},'{2}','{3}','{4}'",
                alm.Archivo.NombreArchivo,
                alm.Archivo.IdDirectorioPadre,
                alm.Extension,
                alm.getLocalStoragePath(),
                alm.MD5
                );
        }

        /// <summary>
        /// Desarrollar recursivamente todas las carpetas y subcarpetas de un directorio particular
        /// </summary>
        /// <param name="idDirectorio"></param>
        /// <returns></returns>
        internal string DesarrollarDirectorioRecursivo(int idDirectorio)
        {
            return string.Format("exec DesarrollarDirectorioRecursivo {0}", idDirectorio);
        }

        /// <summary>
        /// Verificar si dentro del directorioPadre ya existe un archivo con el mismo nombre
        /// </summary>
        /// <param name="alm">Objeto Almacenamiento</param>
        /// <returns></returns>
        internal string VerificarNombresDuplicados(Almacenamiento alm)
        {
            return string.Format("exec VerificarNombresDuplicados '{0}','{1}',{2},{3}",
                alm.Archivo.NombreArchivo,
                alm.Extension,
                alm.Archivo.DirectorioPadre.IdDirectorioPadre,
                alm.Archivo.DirectorioPadre.IdDirectorioRaiz);
        }

        /// <summary>
        /// Verificar existencia de un MD5
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        internal string VerificarMD5(string md5)
        {
            return string.Format("exec VerificarMD5 '{0}'", md5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public string CrearSubDirectorio(Directorio dir)
        {
            return string.Format("exec CrearSubDirectorio {0},{1},'{2}','{3}'", dir.IdDirectorioPadre, dir.IdDirectorioRaiz, dir.NombreDirectorio, dir.DescriptorDirectorio);
        }


        /// <summary>
        /// Obtener metadatos de un archivo para una versión particular o todas sus versiones.
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <param name="versionArchivo"></param>
        /// <returns></returns>
        public string GetMetadata(int idArchivo, int versionArchivo)
        {
            return string.Format("exec GetMetadata {0},{1}", idArchivo, versionArchivo);
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