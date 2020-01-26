using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Verificadores
{
	public class ValidadorPaqueteria : IValidadorPedido
	{
		private IValidadorPedido validadorPedido;

		public void AgregarSiguienteValidacion(IValidadorPedido _validadorPedido)
		{
			validadorPedido = _validadorPedido;
		}

		public string RealizarValidacion(List<PedidoDTO> _lstPedidos, List<IEmpresa> _lstEmpresas)
		{
			string cMensaje = string.Empty;
			List<string> lstMensajes = new List<string>();

			var lstPaqueteriaNoExistentes = (from pedido in _lstPedidos
											 let lExistePaqueteria = _lstEmpresas.Exists(e => e.empresaDTO.cNombre.ToLower().Trim() == pedido.cPaqueteria.ToLower().Trim())
											 where !lExistePaqueteria
											 select pedido.cPaqueteria).ToList();
			if (lstPaqueteriaNoExistentes.Any())
			{
				lstMensajes = lstPaqueteriaNoExistentes.Select(p => $"La Paquetería: {p} no se encuentra registrada en nuestra red de distribución.").ToList();
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
