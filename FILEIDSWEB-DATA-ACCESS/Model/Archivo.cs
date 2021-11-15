using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Archivo
    {

		public int IdArchivo { get; set; }
		public string NombreArchivo { get; set; }
		public int IdRevisionLevel { get; set; }
		public int IdDirectorioPadre { get; set; }
		public bool EsActivo { get; set; }

		public string Revision { get; set; }

		/// <summary>
		/// Objeto directorio padre completo.
		/// </summary>
		public Directorio DirectorioPadre { get; set; }
	}
}
