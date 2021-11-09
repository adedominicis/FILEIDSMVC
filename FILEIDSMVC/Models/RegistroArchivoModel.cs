using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Models
{
    /// <summary>
    /// Model para vista de registro de archivos
    /// </summary>
    public class RegistroArchivoModel
    {
       
        public RegistroArchivoModel()
        {

        }

        #region Campos privados

        #endregion

        #region Propiedades públicas

        /// <summary>
        /// Id del proyecto en el que se carga el archivo
        /// </summary>
        public int IdProyecto { get; set; }

        /// <summary>
        /// Nombre del proyecto.
        /// </summary>
        public string NombreProyecto { get; set; }

        /// <summary>
        /// Nombre del archivo.
        /// Se ha modificado la logica general de FILEIDS para que el nombre del archivo no sea la descripción en español
        /// En función de eso, este campo representa el nombre que será reemplazado en el archivo subido.
        /// </summary>
        [Display(Name = "Nombre del archivo")]
        [MaxLength(50, ErrorMessage = "El nombre del archivo no debe exceder 50 caracteres")]
        public string NombreArchivo { get; set; }

        /// <summary>
        /// Descriptor en español
        /// </summary>
        [MaxLength(500,ErrorMessage ="La descripción no debe exceder 500 caracteres"), 
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
        /// Listado de proyectos existentes en el sistema.
        /// </summary>


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