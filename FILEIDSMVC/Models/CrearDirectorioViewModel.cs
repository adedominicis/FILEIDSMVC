using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    public class CrearDirectorioViewModel
    {
        #region Metadata
        public int IdDirectorioPadre { get; set; }
        public int IdDirectorioRaiz { get; set; }
        public string NombreDirectorioPadre { get; set; }
        #endregion


        #region Campos validables.

        [MaxLength(50, ErrorMessage = "El nombre no debe exceder 50 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre no debe ser menor a 2 caracteres")]
        [Display(Name = "Nombre carpeta")]
        [Required (ErrorMessage ="Este campo es requerido")]
        public string NombreNuevoDirectorio { get; set; }

        [MaxLength(500, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
        [MinLength(2, ErrorMessage = "La descripción no debe ser menor a 2 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string DescriptorNuevoDirectorio { get; set; }

        #endregion
    }
}