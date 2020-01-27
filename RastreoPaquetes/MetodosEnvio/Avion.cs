using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.MetodosEnvio
{
	public class Avion : IMetodoEnvio
	{
		public MetodoEnvioDTO metodoEnvioDTO { get; }

		public Avion(MetodoEnvioDTO _metodoEnvioDTO)
		{
			metodoEnvioDTO = _metodoEnvioDTO;
		}

		public decimal CalcularCostoEnvio(decimal _dDistancia, decimal _dMargenUtilidad)
		{
			decimal dCostoEnvio = 0m;
			decimal dProporcionMargen = 0m;

			dProporcionMargen = 1 + _dMargenUtilidad;
			dCostoEnvio = (metodoEnvioDTO.dCostoKilometro * _dDistancia) * dProporcionMargen;

			return dCostoEnvio;
		}

		public decimal CalcularTiempoTraslado(decimal _dDistancia)
		{
			decimal dTiempoTraslado = _dDistancia / metodoEnvioDTO.dVelocidadEntrega;

			return dTiempoTraslado;
		}

		public DateTime ObtenerFechaEntrega(DateTime _dtPedido, decimal _dTiempoTranslado)
		{
			DateTime dtEntrega;
			double dHorasTranslado = Convert.ToDouble(_dTiempoTranslado);

			dtEntrega = _dtPedido.AddHours(dHorasTranslado);

			return dtEntrega;
		}
	}
}
