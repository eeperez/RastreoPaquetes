using RastreoPaquetes.Interfaces;
using RastreoPaquetes.RangosTiempo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Creadores
{
	public class CreadorRangosTiempo
	{
		public IRangoTiempo ConfigurarRangosTiempo()
		{
			IRangoTiempo rangoMeses = new RangoMeses();
			IRangoTiempo rangoDias = new RangoDias();
			IRangoTiempo rangoHoras = new RangoHoras();
			IRangoTiempo rangoMinutos = new RangoMinutos();

			rangoMeses.AgregarSiguiente(rangoDias);
			rangoDias.AgregarSiguiente(rangoHoras);
			rangoHoras.AgregarSiguiente(rangoMinutos);

			return rangoMeses;
		}
	}
}
