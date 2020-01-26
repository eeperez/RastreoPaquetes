using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.Colores
{
	public class ColorVerde : IColorRespuesta
	{
		public void AplicarColorTexto()
		{
			Console.ForegroundColor = ConsoleColor.Green;
		}
	}
}
