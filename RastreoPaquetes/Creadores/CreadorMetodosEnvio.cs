using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.MetodosEnvio;
using System.Collections.Generic;

namespace RastreoPaquetes.Creadores
{
	public class CreadorMetodosEnvio
	{
		public List<IMetodoEnvio> CrearMetodoEnvio()
		{
			List<IMetodoEnvio> lstMetodoEnvios = new List<IMetodoEnvio>();
			MetodoEnvioDTO barcoDTO = new MetodoEnvioDTO { cNombre = "Barco", dCostoKilometro = 1m, dVelocidadEntrega = 46m };
			MetodoEnvioDTO trenDTO = new MetodoEnvioDTO { cNombre = "Tren", dCostoKilometro = 5m, dVelocidadEntrega = 80m };
			MetodoEnvioDTO avionDTO = new MetodoEnvioDTO { cNombre = "Avión", dCostoKilometro = 10m, dVelocidadEntrega = 600m };

			lstMetodoEnvios.Add(new Barco(barcoDTO));
			lstMetodoEnvios.Add(new Tren(trenDTO));
			lstMetodoEnvios.Add(new Avion(avionDTO));

			return lstMetodoEnvios;
		}
	}
}
