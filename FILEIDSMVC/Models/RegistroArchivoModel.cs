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
       
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="listaProyectos">Lista de tuplas (String: Value, String: Text) 
        /// que es convertible a SelectList para llenar comboboxes</param>
        public RegistroArchivoModel(List<(string Value, string Text)> listaProyectos)
        {
            proyectos = new List<SelectListItem>();
            //Llenar el listado de proyectos y en general cualquier otro combobox que derive de este model
            proyectos.Clear();
            foreach (var item in listaProyectos)
            {
                proyectos.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
        }

        public RegistroArchivoModel()
        {

        }

        #region Campos privados
        private List<SelectListItem> proyectos;

        #endregion

        #region Propiedades públicas
        /// <summary>
        /// Descriptor en español
        /// </summary>
        [MaxLength(100), MinLength(4)]
        [Required(ErrorMessage = "Se requiere una descripción entre 4 y 100 caracteres")]
        [Display(Name = "Descripción")]
        public string DescriptorEs { get; set; }

        /// <summary>
        /// Descriptor en ingles
        /// </summary>
        [Required(ErrorMessage = "Se requiere una descripción entre 4 y 100 caracteres")]
        [Display(Name = "Description")]
        public string DescriptorEn { get; set; }
        
        /// <summary>
        /// Descriptor adicional o comentarios
        /// </summary>
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

        [Display(Name = "Proyecto asociado")]
        public List<SelectListItem> Proyectos
        {
            get { return proyectos; }
        }
        #endregion

        /// <summary>
        /// Id del proyecto seleccionado en el droplist
        /// </summary>
        public string IdProyecto { get; set; }

        /// <summary>
        /// Ruta del archivo
        /// </summary>
        [Display(Name = "Cargar Archivo")]
        public string NombreArchivo { get; set; }

        /// <summary>
        /// Extensión del archivo
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Archivo subido
        /// </summary>
        public HttpPostedFileBase Archivo { get; set; }
    }
}