using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.DTOs
{
	public class PedidoDTO
	{
		public string cOrigen { get; set; }

		public string cDestino { get; set; }

		public decimal dDistancia { get; set; }

		public string cPaqueteria { get; set; }

		public string cMedioTransporte { get; set; }

		public DateTime dtPedido { get; set; }

		public PedidoDTO()
		{
			cOrigen = string.Empty;
			cDestino = string.Empty;
			dDistancia = 0m;
			cPaqueteria = string.Empty;
			cMedioTransporte = string.Empty;
			dtPedido = DateTime.Now;
		}
	}
}
