using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Interfaces
{
	public interface IVerificadorEstadoPaquete
	{
		DateTime dtEntrega { get; set; }

		DateTime dtActual { get; }

		bool VerificarPaqueteEntregado();

		string VerificarSiPaqueteSalio();

		string VerificarSiPaqueteLlego();

		string VerificarPrevioTiempoPaquete();

		string VerificarPrevioCostoPaquete();
	}
}
