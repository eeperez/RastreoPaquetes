using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.Empresas
{
	public class Fedex : IEmpresa
	{
		public EmpresaDTO empresaDTO { get; }

		public IMetodoEnvio metodoEnvio { get; set; }

		public Fedex(IMetodoEnvio _metodoEnvio, EmpresaDTO _empresaDTO)
		{
			metodoEnvio = _metodoEnvio;
			empresaDTO = _empresaDTO;
		}

		public string CalcularRangoTiempo(PedidoDTO _pedidoDTO, DateTime _dtActual, IRangoTiempo _rangoTiempo)
		{
			string cRango;

			decimal dTiempoTraslado = metodoEnvio.CalcularTiempoTraslado(_pedidoDTO.dDistancia);
			DateTime dtEntrega = metodoEnvio.ObtenerFechaEntrega(_pedidoDTO.dtPedido, dTiempoTraslado);
			cRango = _rangoTiempo.ObtenerRangoTiempo(dtEntrega, _dtActual);

			return cRango;
		}

		public string ObtenerCosto(decimal _dDistancia, IVerificadorEstadoPaquete _verificadorEstado)
		{
			string cRespuestaCosto = string.Empty;

			string cMensajeCosto = _verificadorEstado.VerificarPrevioCostoPaquete();
			decimal dCostoEnvio = metodoEnvio.CalcularCostoEnvio(_dDistancia, empresaDTO.dMargenUtilidad);

			cRespuestaCosto = $"{cMensajeCosto} un costo de ${dCostoEnvio.ToString()}";

			return cRespuestaCosto;
		}

		public string ObtenerEstadoPaquete(PedidoDTO _pedidoDTO, IVerificadorEstadoPaquete _verificadorEstado)
		{
			string cEstado = string.Empty;
			decimal dTiempoTraslado = metodoEnvio.CalcularTiempoTraslado(_pedidoDTO.dDistancia);
			DateTime dtEntrega = metodoEnvio.ObtenerFechaEntrega(_pedidoDTO.dtPedido, dTiempoTraslado);
			_verificadorEstado.dtEntrega = dtEntrega;

			string cMensajeSalio = _verificadorEstado.VerificarSiPaqueteSalio();
			string cMensajeLlego = _verificadorEstado.VerificarSiPaqueteLlego();
			string cMensajeHace = _verificadorEstado.VerificarPrevioTiempoPaquete();

			cEstado = $"Tu paquete {cMensajeSalio} de {_pedidoDTO.cOrigen} y {cMensajeLlego} a {_pedidoDTO.cDestino} {cMensajeHace}";

			return cEstado;
		}
	}
}
