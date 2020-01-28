using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.RangosTiempo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetesUTests.RangosTiempo
{
	[TestClass]
	public class RangoMesesUTests
	{
		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasUnMes_RangoUnMes()
		{
			//Arrange
			string cRangoEsperado = "1 mes(es)";
			DateTime dtEntrega = new DateTime(2020, 01, 27);
			DateTime dtActual = new DateTime(2020, 02, 27);
			var SUT = new RangoMeses();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnMes_Rango3Meses()
		{
			//Arrange
			string cRangoEsperado = "3 mes(es)";
			DateTime dtEntrega = new DateTime(2020, 01, 27);
			DateTime dtActual = new DateTime(2020, 04, 27);
			var SUT = new RangoMeses();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaFechasMasDeUnMesInversa_Rango3Meses()
		{
			//Arrange
			string cRangoEsperado = "3 mes(es)";
			DateTime dtEntrega = new DateTime(2020, 04, 27);
			DateTime dtActual = new DateTime(2020, 01, 27);
			var SUT = new RangoMeses();

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
			var SUT = new RangoMeses();

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}

		[TestMethod]
		public void ObtenerRangoTiempo_DiferenciaMesesIgualCero_ObtieneRangoSiguiente()
		{
			//Arrange
			string cRangoEsperado = "dias";
			DateTime dtEntrega = new DateTime(2020, 01, 27);
			DateTime dtActual = new DateTime(2020, 01, 28);
			var DOCIRangoTiempoMinutos = new Mock<IRangoTiempo>();
			DOCIRangoTiempoMinutos.Setup(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns("dias");
			var SUT = new RangoMeses();
			SUT.AgregarSiguiente(DOCIRangoTiempoMinutos.Object);

			//Act
			string cRango = SUT.ObtenerRangoTiempo(dtEntrega, dtActual);

			//Assert
			Assert.AreEqual(cRangoEsperado, cRango);
		}
	}
}
