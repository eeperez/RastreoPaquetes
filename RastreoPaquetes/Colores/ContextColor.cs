using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetes.Colores
{
	public class ContextColor
	{
		private IColorRespuesta colorRespuesta;

		public void AgregarColor(IColorRespuesta _colorRespuesta)
		{
			colorRespuesta = _colorRespuesta ?? throw new ArgumentNullException("Es necesrio indicar un color no nulo");
		}

		public void AplicarColorTexto()
		{
			colorRespuesta.AplicarColorTexto();
		}
	}
}
