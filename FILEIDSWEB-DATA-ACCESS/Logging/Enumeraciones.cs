using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Logging
{
    public enum EnumMensajes
    {
        loginError,
        camposRequeridos,
        dataError,
        registroExiste,
        registroNoExiste,
        errorSQL,
        formularioIncompleto,
        registroExitoso,
        registroFallido,
        registroModificado,
        registroEliminado,
        errorSubirArchivo,
        errorBorrarArchivo,
        errorEnConexionDB
    }
}
