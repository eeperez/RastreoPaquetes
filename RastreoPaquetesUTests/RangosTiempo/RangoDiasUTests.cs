using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.RangosTiempo;
using System;

namespace RastreoPaquetesUTests.RangosTiempo
{
	[TestClass]
	public class RangoDiasUTests
	{
		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasUnDia_Rango1dia()
		{
			//Arrange
			string cRangoEsperado = "1 día";
			DateTime dtEntrega = new DateTime(2020, 01, 26);
			DateTime dtActual = new DateTime(2020, 01, 27);
			var SUT = new RangoDias();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnDia_Rango3dias()
		{
			//Arrange
			string cRangoEsperado = "3 días";
			DateTime dtEntrega = new DateTime(2020, 01, 24);
			DateTime dtActual = new DateTime(2020, 01, 27);
			var SUT = new RangoDias();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnDiaInversa_Rango3dias()
		{
			//Arrange
			string cRangoEsperado = "3 días";
			DateTime dtEntrega = new DateTime(2020, 01, 27);
			DateTime dtActual = new DateTime(2020, 01, 24);
			var SUT = new RangoDias();

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
			DateTime dtEntrega = new DateTime(2020, 01, 27);
			DateTime dtActual = new DateTime(2020, 01, 27);
			var SUT = new RangoDias();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaDiasIgualCero_ObtieneRangoSiguiente()
		{
			//Arrange
			string cRangoEsperado = "horas";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 12, 00, 00);
			var DOCIRangoTiempoHoras = new Mock<IRangoTiempo>();
			DOCIRangoTiempoHoras.Setup(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns("horas");
			var SUT = new RangoDias();
			SUT.AgregarSiguiente(DOCIRangoTiempoHoras.Object);

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}
	}
}
