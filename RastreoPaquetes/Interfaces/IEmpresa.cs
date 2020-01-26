using RastreoPaquetes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Interfaces
{
	public interface IEmpresa
	{
		EmpresaDTO empresaDTO { get; }

		IMetodoEnvio metodoEnvio { get; set; }

		string ObtenerEstadoPaquete(PedidoDTO _pedidoDTO, IVerificadorEstadoPaquete _verificadorEstado);

		string ObtenerCosto(decimal _dDistancia, IVerificadorEstadoPaquete _verificadorEstado);

		string CalcularRangoTiempo(PedidoDTO _pedidoDTO, DateTime _dtActual, IRangoTiempo _rangoTiempo);
	}
}
