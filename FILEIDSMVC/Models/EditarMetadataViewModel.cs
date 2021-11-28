using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    /// <summary>
    /// ViewModel de una vista en la que se editan los metadatos de un archivo particular.
    /// </summary>
    public class EditarMetadataViewModel
    {
        #region Propiedades Publicas
        /// <summary>
        /// Versiones del archivo tratado
        /// </summary>
        [Display(Name = "Versión")]
        public List<int> Versiones { get; private set; }

        /// <summary>
        /// Nombre del directorio al que pertenece el archivo
        /// </summary>
        public string NombreDirectorio { get; set; }

        /// <summary>
        /// Identificador unico del archivo.
        /// </summary>

        public int IdArchivo { get; set; }

        /// <summary>
        /// id_carpeta_padre en tabla ARCHIVOS
        /// IdExtension de la carpeta en la que se encuentra el archivo.
        /// </summary>
        public int IdDirectorioPadre { get; set; }

        /// <summary>
        /// Descriptor en español
        /// </summary>
        [MaxLength(500, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
        [MinLength(2, ErrorMessage = "La descripción no debe ser menor a 2 caracteres")]
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
        /// Numero de parte OEM
        /// </summary>
        [Display(Name = "N° de parte OEM")]
        public string OemSku { get; set; }


        /// <summary>
        /// Extensión del archivo
        /// </summary>
        public string Extension { get; set; }

        public string NombreArchivo { get; set; }
        #endregion


    }
}