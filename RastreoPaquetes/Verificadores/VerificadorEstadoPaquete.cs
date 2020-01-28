using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Verificadores
{
	public class VerificadorEstadoPaquete : IVerificadorEstadoPaquete
	{
		public DateTime dtEntrega { get; set; }

		public DateTime dtActual { get; }

		public VerificadorEstadoPaquete(DateTime _dtActual)
		{
			dtEntrega = DateTime.Now;
			dtActual = _dtActual;
		}

		public bool VerificarPaqueteEntregado()
		{
			return dtEntrega < dtActual;
		}

		public string VerificarPrevioCostoPaquete()
		{
			string cMensaje = "tendrá";

			if (VerificarPaqueteEntregado())
				cMensaje = "tuvo";

			return cMensaje;
		}

		public string VerificarPrevioTiempoPaquete()
		{
			string cMensaje = "dentro de";

			if (VerificarPaqueteEntregado())
				cMensaje = "hace";

			return cMensaje;
		}

		public string VerificarSiPaqueteLlego()
		{
			string cMensaje = "llegará";

			if (VerificarPaqueteEntregado())
				cMensaje = "llegó";

			return cMensaje;
		}

		public string VerificarSiPaqueteSalio()
		{
			string cMensaje = "ha salido";

			if (VerificarPaqueteEntregado())
				cMensaje = "salió";

			return cMensaje;
		}
	}
}
