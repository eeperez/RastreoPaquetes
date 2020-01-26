using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Verificadores
{
	public class ValidadorMedioTransporteDelPedido : IValidadorPedido
	{
		private IValidadorPedido validadorPedido;

		public void AgregarSiguienteValidacion(IValidadorPedido _validadorPedido)
		{
			validadorPedido = _validadorPedido;
		}

		public string RealizarValidacion(List<PedidoDTO> _lstPedidos, List<IEmpresa> _lstEmpresas)
		{
			string cMensaje = string.Empty;

			var lstPedidosMedioNoValido = (from pedido in _lstPedidos
										   let lExisteMedioParaEmpresa = 
											_lstEmpresas.Exists(e => e.empresaDTO.cNombre.ToLower().Trim() == pedido.cPaqueteria.ToLower().Trim() && 
											e.metodoEnvio.metodoEnvioDTO.cNombre.ToLower().Trim() == pedido.cMedioTransporte.ToLower().Trim())
										   where !lExisteMedioParaEmpresa
										   select pedido).ToList();
			if (lstPedidosMedioNoValido.Any())
			{
				List<string> lstMensajes = lstPedidosMedioNoValido
					.Select(p => $"{p.cPaqueteria} no ofrece el servicio de transporte {p.cMedioTransporte}, te recomendamos cotizar con otra empresa.").ToList();
				cMensaje = string.Join("\n", lstMensajes);
			}
			else
			{
				if (validadorPedido != null)
					cMensaje = validadorPedido.RealizarValidacion(_lstPedidos, _lstEmpresas);
			}

			return cMensaje;
		}
	}
}
