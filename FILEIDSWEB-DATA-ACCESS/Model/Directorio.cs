using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Directorio
    {
        /// <summary>
        /// Nombre del directorio raiz del proyecto
        /// </summary>

        public string NombreDirectorio { get; set; }

        /// <summary>
        /// Descriptor del directorio. Una suerte de comentario.
        /// </summary>
     
        public string DescriptorDirectorio { get; set; }


        /// <summary>
        /// Identificador del directorio
        /// </summary>

        public int IdDirectorio { get; set; }

        /// <summary>
        /// Determina si el directorio no ha sido eliminado.
        /// </summary>
        public bool DirectorioActivo { get; set; }

        /// <summary>
        /// Directorio en el que está contenido el actual.
        /// </summary>
        public int IdDirectorioPadre { get; set; }

        /// <summary>
        /// Directorio raiz al que pertenece el actual
        /// </summary>
        public int IdDirectorioRaiz { get; set; }

        /// <summary>
        /// Profundidad del directorio donde 0 es el directorio raiz padre.
        /// </summary>
        public int Profundidad { get; set; }

    }
}
