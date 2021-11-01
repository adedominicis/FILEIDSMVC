using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Proyecto
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

    }
}
