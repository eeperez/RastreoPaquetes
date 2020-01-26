using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.RangosTiempo
{
	public class RangoDias : IRangoTiempo
	{
		private IRangoTiempo rangoTiempo;

		public void AgregarSiguiente(IRangoTiempo _rangoTiempo)
		{
			rangoTiempo = _rangoTiempo ?? throw new ArgumentNullException("Es necesario indicar un rango de tiempo");
		}

		public string ObtenerRangoTiempo(DateTime _dtVerificar, DateTime _dtVerificadora)
		{
			string cRango = string.Empty;
			int iDias;
			string cPlural = "s";

			var diferencia = _dtVerificadora - _dtVerificar;
			iDias = Math.Abs(diferencia.Days);
			if (iDias > 0)
			{
				cPlural = iDias == 1 ? string.Empty : cPlural;
				cRango = $"{iDias} día{cPlural}";
			}
			else
			{
				if (rangoTiempo != null)
					cRango = rangoTiempo.ObtenerRangoTiempo(_dtVerificar, _dtVerificadora);
			}

			return cRango;
		}
	}
}
