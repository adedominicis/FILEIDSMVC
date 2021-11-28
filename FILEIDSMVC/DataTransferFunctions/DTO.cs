using FILEIDSMVC.Models;
using FILEIDSWEB_DATA_ACCESS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.DataTransferFunctions
{
    /// <summary>
    /// Funciones de transferencia entre la capa MVC hacia la capa Data Access
    /// </summary>
    public class DTO
    {

        /// <summary>
        /// DATA TRANSFER OBJECT:
        /// Desde: ViewModel de registro de archivos -RegistrarArchivoViewModel-
        /// Hasta: Model del Data Access -Almacenamiento-
        /// </summary>
        /// <param name="ravm">ViewModel del tipo -RegistrarArchivoViewModel-</param>
        /// <returns></returns>
        public static Almacenamiento RegArchivo_AlmacenamientoDTO(RegistrarArchivoViewModel ravm)
        {
            //Objetos de almacenamiento.
            Metadata met = new Metadata()
            {
                DescriptorEs = ravm.DescriptorEs,
                DescriptorEn = ravm.DescriptorEn,
                DescriptorExtra = ravm.DescriptorExtra,
                Oemsku = ravm.OemSku
            };
            Directorio dir = new Directorio()
            {
                NombreDirectorio = ravm.NombreDirectorio,
                IdDirectorioPadre = ravm.IdDirectorioPadre
            };

            Archivo arc = new Archivo()
            {
                IdDirectorioPadre = ravm.IdDirectorioPadre,
                NombreArchivo = ravm.NombreArchivo,
                DirectorioPadre = dir,
                Extension = ravm.Extension
            };
            Almacenamiento alm = new Almacenamiento(ConfigurationManager.AppSettings["FileCachePath"].ToString())
            {
                ArchivoFisico = ravm.ArchivoSubido,
                Extension = ravm.Extension,
                Archivo = arc,
                Metadata = met
            };

            return alm;
        }

        /// <summary>
        /// DATA TRANSFER OBJECT
        /// Desde: Consulta Ajax en controlador metadatos -MetadataController-
        /// Hasta: Model del data access -Almacenamiento-
        /// [TODO]
        /// Mejoras posibles: Recibir el view model que corresponde a los metadatos en lugar de los datos discretos.
        /// </summary>
        public static Almacenamiento AjaxMetadata_AlmacenamientoDTO(int IdArchivo, string DescriptorEs, string DescriptorEn, string DescriptorExtra, string @OemSku)
        {
            //Objetos de almacenamiento.
            Metadata met = new Metadata()
            {
                DescriptorEs = DescriptorEs,
                DescriptorEn = DescriptorEn,
                DescriptorExtra = DescriptorExtra,
                Oemsku = OemSku
            };

            Archivo arc = new Archivo()
            {
                IdArchivo = IdArchivo
            };
            Almacenamiento alm = new Almacenamiento(ConfigurationManager.AppSettings["FileCachePath"].ToString())
            {
                Archivo = arc,
                Metadata = met
            };
            return alm;
        }
    }
}