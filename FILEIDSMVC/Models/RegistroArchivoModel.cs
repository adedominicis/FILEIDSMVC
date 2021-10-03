using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Models
{
    /// <summary>
    /// Model para registro del archivo.
    /// </summary>
    public class RegistroArchivoModel
    {
        private List<SelectListItem> extensiones;

        public RegistroArchivoModel()
        {
            extensiones = new List<SelectListItem>();
        }
        

        public string Ruta { get; set; }
        public int Id { get; set; }
        public string DescriptorEs { get; set; }
        public string DescriptorEn { get; set; }
        public string OemSku { get; set; }

        public string FormattedId { get; set; }

        public string IdExtension { get; set; }
        
        public List<SelectListItem> Extensiones
        {
            get { return extensiones; }
        }

        public void setExtensiones(List<(string Value, string Text)> lista){

            extensiones.Clear();
            foreach (var item in lista)
            {
                extensiones.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

        }

    }
}