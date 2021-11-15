using FILEIDSWEB_DATA_ACCESS.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Models
{
    /// <summary>
    /// ViewModel para vista de proyectos.
    /// Conecta directamente con los objetos relevantes del modelo en e FILEIDSWEB_DATA_ACCESS
    /// </summary>
    public class ProyectosViewModel
    {

        public ProyectosViewModel()
        {

        }

        #region Campos privados

        Archivo archivo = new Archivo();
        Almacenamiento almacenamiento = new Almacenamiento();
        Directorio directorio = new Directorio();
        Metadata metadata = new Metadata();
        #endregion

        #region Listados activos - Se listan elementos del acceso a datos directamente.
        //Listados activos en la interfaz de proyectos. Se listan directorios raiz, subdirectorios y archivos.
        //Los elementos de las listas son modelos del acceso a datos. No se si esto es correcto o razonable en terminos de coupling.
        //Puede reformularse en terminos de interfaces.

        /// <summary>
        /// Lista de directorios raiz.
        /// </summary>
        public List<Directorio> DirectoriosRaiz { get; set; }

        /// <summary>
        /// Lista de subdirectorios
        /// </summary>
        public List<Directorio> SubDirectorios { get; set; }

        /// <summary>
        /// Lista de archivos
        /// </summary>
        public List<Archivo> Archivos { get; set; }

        /// <summary>
        /// Lista de objetos almacenamiento
        /// </summary>
        public List<Almacenamiento> Almacenamientos { get; set; }

        #endregion

        #region Elementos activos
        /// <summary>
        /// Identificador del directorio raiz activo
        /// </summary>
        public int IdDirectorioRaizActivo { get; set; }

        /// <summary>
        /// Nombre del directorio
        /// </summary>
        public string NombreDirectorioRaizActivo { get; set; }

        /// <summary>
        /// Nombre del directorio
        /// </summary>
        public string NombreSubDirectorioActivo { get; set; }

        /// <summary>
        /// Identificador del directorio activo
        /// </summary>
        public int IdSubDirectorioActivo { get; set; }


        #endregion


        #region Elementos nuevos
        /// <summary>
        /// Nombre del directorio nuevo a crear.
        /// </summary>

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El nombre del directorio no debe exceder 50 caracteres")]
        [Required]
        public string NombreDirectorioRaizNuevo { get; set; }

        /// <summary>
        /// Nombre del directorio nuevo a crear.
        /// </summary>

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
        [Required]
        public string DescriptorDirectorioRaizNuevo { get; set; }

        #endregion


    }
}