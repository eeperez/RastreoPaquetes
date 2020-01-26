using RastreoPaquetes.DTOs;
using System.Collections.Generic;

namespace RastreoPaquetes.Interfaces
{
	public interface IConvertidorPedido
	{
		List<PedidoDTO> ConvertirAPedidos(List<string> _lstLineas);
	}
}
