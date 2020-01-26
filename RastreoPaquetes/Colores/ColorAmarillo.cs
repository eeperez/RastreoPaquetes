using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.Colores
{
	public class ColorAmarillo : IColorRespuesta
	{
		public void AplicarColorTexto()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
		}
	}
}
