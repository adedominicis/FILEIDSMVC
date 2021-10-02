using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FILEIDSMVC.Models
{
    public class Archivo
    {
        public string Ruta { get; set; }
        public int IdExtension { get; set; }
        public int Id { get; set; }
        public string DescriptorEs { get; set; }
        public string DescriptorEn { get; set; }
        public string OemSku { get; set; }
    }
}