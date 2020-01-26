using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.DTOs
{
	public class MetodoEnvioDTO
	{
		public string cNombre { get; set; }

		public decimal dCostoKilometro { get; set; }

		public decimal dVelocidadEntrega { get; set; }
	}
}
