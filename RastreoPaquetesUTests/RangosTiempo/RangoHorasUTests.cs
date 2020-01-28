using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.RangosTiempo;
using System;

namespace RastreoPaquetesUTests.RangosTiempo
{
	[TestClass]
	public class RangoHorasUTests
	{
		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasUnaHora_Rango1Hora()
		{
			//Arrange
			string cRangoEsperado = "1 hora";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 11, 00, 00);
			var SUT = new RangoHoras();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnaHora_Rango3Horas()
		{
			//Arrange
			string cRangoEsperado = "3 horas";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 13, 00, 00);
			var SUT = new RangoHoras();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnaHoraInversa_Rango3dias()
		{
			//Arrange
			string cRangoEsperado = "3 horas";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 13, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 00, 00);
			var SUT = new RangoHoras();

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
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 00, 00);
			var SUT = new RangoHoras();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaHorasIgualCero_ObtieneRangoSiguiente()
		{
			//Arrange
			string cRangoEsperado = "minutos";
			DateTime dtEntrega = new DateTime(2020, 01, 27, 10, 00, 00);
			DateTime dtActual = new DateTime(2020, 01, 27, 10, 20, 00);
			var DOCIRangoTiempoMinutos = new Mock<IRangoTiempo>();
			DOCIRangoTiempoMinutos.Setup(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns("minutos");
			var SUT = new RangoHoras();
			SUT.AgregarSiguiente(DOCIRangoTiempoMinutos.Object);

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}
	}
}
