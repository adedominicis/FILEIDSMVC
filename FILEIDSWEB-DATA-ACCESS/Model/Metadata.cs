using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Metadata

    // Bundle de propiedades personalizadas
    {

        public Metadata()
        {
        }

        #region Propiedades públicas
        public string IdMetadata { get; set; }
        public string Oemsku { get; set; }
        public string DescriptorEn { get; set; }
        public string DescriptorEs { get; set; }
        public string DescriptorExtra { get; set; }

        #endregion
        //public string PartId { get; set; }

        //public int IdExtension
        //{
        //    get
        //    {
        //        return idExtension;
        //    }
        //    set
        //    {
        //        idExtension = value;
        //    }
        //}
        //public int IdProyecto
        //{
        //    get
        //    {
        //        return idProyecto;
        //    }
        //    set
        //    {
        //        idProyecto = value;
        //    }
        //}
        //public string Extension
        //{
        //    get { return extension; }
        //    set { extension = value; }

        //}

        //public string NombreArchivo {
        //    get { return nombreArchivo; }
        //    set { nombreArchivo = value; }
        //}

        ///// <summary>
        ///// Revisar 
        ///// </summary>
        ///// <returns></returns>
        //public string getNombreArchivoFormateado()
        //{
        //    string nombre = getFormattedID() + " - " + descriptorEs;
        //    return nombre;
        //}
        //public string getFormattedID()
        //{
        //    if (id != null)
        //    {
        //        return id.PadLeft(6, '0').Insert(3, "-");
        //    }
        //    return string.Empty;


        //}
    }
}
