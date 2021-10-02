using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FILEIDSWEB_DATA_ACCESS.Model
{
    public class Ruta
    {
		private string idArchivo;
		private string strRuta;
		private string revLevel;
		private string id;
		private string revLetter;
		private string md5;


		public string MD5
		{
			get { return md5; }
			set { md5 = value; }
		}

		public string RevLetter
		{
			get { return revLetter; }
			set { revLetter = value; }
		}

		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string IdArchivo
		{
			get { return idArchivo; }
			set { idArchivo = value; }
		}

		public string StrRuta
		{
			get { return strRuta; }
			set { strRuta = value; }
		}

		public string RevLevel
		{
			get { return revLevel; }
			set { revLevel = value; }
		}
	}
}
