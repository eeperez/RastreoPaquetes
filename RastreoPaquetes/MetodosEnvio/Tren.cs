using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;

namespace RastreoPaquetes.MetodosEnvio
{
	public class Tren : IMetodoEnvio
	{
		public MetodoEnvioDTO metodoEnvioDTO { get; }

		public Tren(MetodoEnvioDTO _metodoEnvioDTO)
		{
			metodoEnvioDTO = _metodoEnvioDTO;
		}

		public decimal CalcularCostoEnvio(decimal _dDistancia, decimal _dMargenUtilidad)
		{
			decimal dProporcionMargen = 1 + _dMargenUtilidad;
			decimal dCostoEnvio = (metodoEnvioDTO.dCostoKilometro * _dDistancia) * dProporcionMargen;

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
