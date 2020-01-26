using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Verificadores
{
	public class ValidadorInformacion : IValidadorPedido
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

			if (!_lstPedidos.Any())
				lstMensajes.Add("Es necesario indicar pedido de forma correcta.");

			if (!_lstEmpresas.Any())
				lstMensajes.Add("Es necesario tener minimo una empresa de envíos.");

			if (lstMensajes.Any())
			{
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
