using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.Colores
{
	public class ColorRojo : IColorRespuesta
	{
		public void AplicarColorTexto()
		{
			Console.ForegroundColor = ConsoleColor.Red;
		}
	}
}
