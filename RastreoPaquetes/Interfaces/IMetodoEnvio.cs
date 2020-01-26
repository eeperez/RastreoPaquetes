using RastreoPaquetes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Interfaces
{
	public interface IMetodoEnvio
	{
		MetodoEnvioDTO metodoEnvioDTO { get; }

		decimal CalcularTiempoTraslado(decimal _dDistancia);

		DateTime ObtenerFechaEntrega(DateTime _dtPedido, decimal _dTiempoTranslado);

		decimal CalcularCostoEnvio(decimal _dDistancia, decimal _dMargenUtilidad);
	}
}
