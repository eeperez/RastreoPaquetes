using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.RangosTiempo
{
	public class RangoMeses : IRangoTiempo
	{
		private IRangoTiempo rangoTiempo;

		public void AgregarSiguiente(IRangoTiempo _rangoTiempo)
		{
			rangoTiempo = _rangoTiempo ?? throw new ArgumentNullException("Es necesario indicar un rango de tiempo");
		}

		public string ObtenerRangoTiempo(DateTime _dtVerificar, DateTime _dtVerificadora)
		{
			string cRango = string.Empty;

			var diferencia = _dtVerificadora - _dtVerificar;
			if (Math.Abs(diferencia.Days) > 30)
			{
				int idif = diferencia.Days / 30;
				cRango = $"{Math.Abs(idif)} Mes(es)";
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
