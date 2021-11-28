using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{

    /// <summary>
    /// Modelo de acceso a datos del objeto Archivo.
    /// Se basa en la tabla [dbo].[Archivos] tomando algunos elementos de las tablas con las que tiene FK.
    /// 
    /// Existe un dilema sobre como estructurar la aplicación y hasta que punto los modelos en esta capa
    /// hacen mímica del modelo relacional y en que momento se diferencian o como se conectan con los modelos
    /// usados en el MVC. En teoria, esta conexión debe hacerse a traves de DTOs (Data Transfer Objects) que estan
    /// definidos en el proyecto de front end.
    /// </summary>
    public class Archivo
    {
        public int IdArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public int IdRevisionLevel { get; set; }
        public int IdDirectorioPadre { get; set; }
        public bool EsActivo { get; set; }
        public string Revision { get; set; }
        public int Version { get; set; }
        public int IdMetadata { get; set; }
        public Directorio DirectorioPadre { get; set; }
        public string Extension { get; set; }

    }
}
