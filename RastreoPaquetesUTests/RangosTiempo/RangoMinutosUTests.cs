using Microsoft.VisualStudio.TestTools.UnitTesting;
using RastreoPaquetes.RangosTiempo;
using System;

namespace RastreoPaquetesUTests.RangosTiempo
{
	[TestClass]
	public class RangoMinutosUTests
	{
		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasUnMinuto_RangoUnMinuto()
		{
			//Arrange
			string cRangoEsperado = "1 minuto";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 10, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 11, 00);
			var SUT = new RangoMinutos();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnMinuto_Rango3Minutos()
		{
			//Arrange
			string cRangoEsperado = "3 minutos";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 10, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 13, 00);
			var SUT = new RangoMinutos();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnMinutoInversa_Rango3dias()
		{
			//Arrange
			string cRangoEsperado = "3 minutos";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 13, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 10, 00);
			var SUT = new RangoMinutos();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_FechasIguales_RangoVacio()
		{
			//Arrange
			string cRangoEsperado = "";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 10, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 10, 00);
			var SUT = new RangoMinutos();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}
	}
}
