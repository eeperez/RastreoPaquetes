using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Presentadores
{
	public class PresentadorDatos : IPresentadorDatos
	{
		public Action<string> presentadorDatos { get; set; }

		public PresentadorDatos()
		{
			presentadorDatos = cMensaje => Console.WriteLine(cMensaje);
		}

		public void MostrarDatos(string _cMensaje)
		{
			presentadorDatos(_cMensaje);
		}
	}
}
