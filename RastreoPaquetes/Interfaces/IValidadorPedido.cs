using RastreoPaquetes.DTOs;
using System.Collections.Generic;

namespace RastreoPaquetes.Interfaces
{
	public interface IValidadorPedido
	{
		void AgregarSiguienteValidacion(IValidadorPedido _validadorPedido);

		string RealizarValidacion(List<PedidoDTO> _lstPedidos, List<IEmpresa> _lstEmpresas);
	}
}
