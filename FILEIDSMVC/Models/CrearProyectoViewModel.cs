using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    public class CrearProyectoViewModel
    {
        #region Propiedades a validar.
        /// <summary>
        /// Nombre del directorio nuevo a crear.
        /// </summary>

        [Display(Name = "Nombre del proyecto")]
        [MaxLength(50, ErrorMessage = "El nombre del directorio no debe exceder 50 caracteres")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NombreProyecto { get; set; }

        /// <summary>
        /// Nombre del directorio nuevo a crear.
        /// </summary>

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string DescriptorProyecto { get; set; }

        #endregion
    }
}