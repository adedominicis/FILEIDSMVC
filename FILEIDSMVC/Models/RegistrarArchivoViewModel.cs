using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    public class RegistrarArchivoViewModel
    {
        #region Propiedades Publicas

        /// <summary>
        /// Nombre del directorio al que pertenece el archivo
        /// </summary>
        public string NombreDirectorio { get; set; }

        /// <summary>
        /// Identificador unico del archivo.
        /// </summary>

        public int IdArchivo { get; set; }

        /// <summary>
        /// Nombre del archivo.
        /// </summary>
        [Display(Name = "Nombre del archivo")]
        [MaxLength(50, ErrorMessage = "El nombre del archivo no debe exceder 50 caracteres")]
        public string NombreArchivo { get; set; }

        /// <summary>
        /// id_carpeta_padre en tabla ARCHIVOS
        /// IdExtension de la carpeta en la que se encuentra el archivo.
        /// </summary>
        public int IdCarpetaPadre { get; set; }


        /// <summary>
        /// True para archivos activos, false para archivos eliminados.
        /// </summary>
        public bool EsActivo { get; set; }


        /// <summary>bghnv j'¿
        /// Descriptor en español
        /// </summary>
        [MaxLength(500, ErrorMessage = "La descripción no debe exceder 500 caracteres"),
            MinLength(2, ErrorMessage = "La descripción no debe ser menor a 2 caracteres")]
        [Required(ErrorMessage = "Se requiere una descripción")]
        [Display(Name = "Descripción")]
        public string DescriptorEs { get; set; }

        /// <summary>
        /// Descriptor en ingles
        /// </summary>
        [MaxLength(500, ErrorMessage = "Description should not exceed 500 characters long")]
        [Display(Name = "Description")]
        public string DescriptorEn { get; set; }

        /// <summary>
        /// Descriptor adicional o comentarios
        /// </summary>
        [MaxLength(500, ErrorMessage = "Los comentarios no deben exceder 500 caracteres")]
        [Display(Name = "Comentarios")]
        public string DescriptorExtra { get; set; }

        /// <summary>
        /// Numero de parte OEM, si correspondiese.
        /// </summary>
        [Display(Name = "N° de parte OEM")]
        public string OemSku { get; set; }


        /// <summary>
        /// Ruta del archivo
        /// </summary>
        [Display(Name = "Cargar Archivo")]
        public string NombreArchivoSubido { get; set; }

        /// <summary>
        /// Extensión del archivo
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Archivo subido
        /// </summary>
        public HttpPostedFileBase ArchivoSubido { get; set; }


        #endregion
    }
}