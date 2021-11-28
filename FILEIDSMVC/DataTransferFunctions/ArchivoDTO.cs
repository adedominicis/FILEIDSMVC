using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.DataTransferFunctions
{
    public class ArchivoDTO
    {
		public int IdArchivo { get; set; }
		public string NombreArchivo { get; set; }
		public int IdRevisionLevel { get; set; }
		public int IdDirectorioPadre { get; set; }
		public bool EsActivo { get; set; }
		public string Revision { get; set; }
		public string Extension { get; set; }
	}
}