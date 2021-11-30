using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FILEIDSMVC.Startup))]
namespace FILEIDSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

//Dedicado a la memoria de Marcelo De Dominicis (30-11-1956 - 13-09-2021) quien ha partido de entre nosotros
//Ahora te sientas a la diestra del padre y desde ahi pones tu mano en todo lo que hago y haré siempre.
//Que este y todas mis obras futuras tengan tu venía y la bendición de Dios
//Te encomiendo mi oración para que seas cuidado y bendecido por la eternidad

//Pater noster, qui es in caelis:
//sanctificetur Nomen Tuum;
//adveniat Regnum Tuum;
//fiat voluntas Tua,
//sicut in caelo, et in terra.
//Panem nostrum cotidianum da nobis hodie;
//et dimitte nobis debita nostra,
//sicut et nos dimittimus debitoribus nostris;
//et ne nos inducas in tentationem;
//sed libera nos a Malo.
//Amén

//Soli Deo Gloria
