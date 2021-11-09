using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class FileMetaData

    // Bundle de propiedades personalizadas
    {

        public FileMetaData()
        {
        }

        // Variables de campos de texto en el formulario
        private string id;
        private string oemsku;
        private string descriptorEn;
        private string descriptorEs;
        private string descriptorExtra;

        private string extension;
        private string nombreArchivo;
        private int idExtension;
        private int idProyecto;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public string Oemsku
        {
            get { return oemsku; }
            set
            { oemsku = value; }
        }
        public string DescriptorEn
        {
            get { return descriptorEn; }
            set
            { descriptorEn = value; }
        }
        public string DescriptorEs
        {
            get { return descriptorEs; }
            set { descriptorEs = value; }
        }
        public string DescriptorExtra
        {
            get { return descriptorExtra; }
            set { descriptorExtra = value; }
        }

        //Id's y valores comboboxes


        public int IdExtension
        {
            get
            {
                return idExtension;
            }
            set
            {
                idExtension = value;
            }
        }
        public int IdProyecto
        {
            get
            {
                return idProyecto;
            }
            set
            {
                idProyecto = value;
            }
        }
        public string Extension
        {
            get { return extension; }
            set { extension = value; }

        }

        public string NombreArchivo {
            get { return nombreArchivo; }
            set { nombreArchivo = value; }
        }

        /// <summary>
        /// Revisar 
        /// </summary>
        /// <returns></returns>
        public string getNombreArchivoFormateado()
        {
            string nombre = getFormattedID() + " - " + descriptorEs;
            return nombre;
        }
        public string getFormattedID()
        {
            if (id != null)
            {
                return id.PadLeft(6, '0').Insert(3, "-");
            }
            return string.Empty;


        }
    }
}
