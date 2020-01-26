using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Interfaces
{
	/// <summary>
	/// Rango de tiempo para cadena de responsabilidades
	/// </summary>
	public interface IRangoTiempo
	{
		void AgregarSiguiente(IRangoTiempo _rangoTiempo);

		/// <summary>
		/// Obtiene el rango de tiempo con su unidad de tiempo
		/// </summary>
		/// <param name="_dtVerificar">Fecha que se desea verificar contra la fecha verificadora</param>
		/// <param name="_dtVerificadora">Fecha para comparar</param>
		/// <returns>Tiempo con unidad de tiempo</returns>
		string ObtenerRangoTiempo(DateTime _dtVerificar, DateTime _dtVerificadora);
	}
}
