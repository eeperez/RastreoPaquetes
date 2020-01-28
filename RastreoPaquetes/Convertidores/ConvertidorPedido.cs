using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Convertidores
{
	public class ConvertidorPedido : IConvertidorPedido
	{
		public List<PedidoDTO> ConvertirAPedidos(List<string> _lstLineas)
		{
			List<PedidoDTO> lstPedidos;

			lstPedidos = (from linea in _lstLineas
						  let datos = linea.Split(',')
						  where datos.Count() >= 6
						  select new PedidoDTO
						  {
							  cOrigen = datos[0],
							  cDestino = datos[1],
							  dDistancia = Convert.ToDecimal(datos[2]),
							  cPaqueteria = datos[3],
							  cMedioTransporte = datos[4],
							  dtPedido = Convert.ToDateTime(datos[5])
						  }).ToList();

			return lstPedidos;
		}
	}
}
