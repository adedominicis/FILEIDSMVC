using FILEIDSWEB_DATA_ACCESS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    public class ProyectosModel
    {
        /// <summary>
        /// Nombre del directorio.
        /// No es la ruta, la ruta se construye a partir de las relaciones padre hijo.
        /// </summary>

        [Display(Name = "Proyecto")]
        public string NombreDirectorio { get; set; }

        /// <summary>
        /// Descriptor del directorio. Una suerte de comentario.
        /// </summary>
        [Display(Name = "Descripción")]
        public string DescriptorDirectorio { get; set; }

        /// <summary>
        /// true para existente, false para borrado.
        /// </summary>
        public bool DirectorioActivo { get; set; }

        /// <summary>
        /// Id interno del directorio
        /// </summary>
        public int IdDirectorio { get; set; }

        /// <summary>
        /// Listado de proyectos traidos de la DB.
        /// </summary>
        public List<Proyecto> ListaDirectorios { get; set; }
    }
}