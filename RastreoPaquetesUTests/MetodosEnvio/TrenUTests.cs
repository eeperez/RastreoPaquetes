using Microsoft.VisualStudio.TestTools.UnitTesting;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.MetodosEnvio;
using System;

namespace RastreoPaquetesUTests
{
	[TestClass]
	public class TrenUTests
	{
		[TestMethod]
		public void CalcularTiempoTraslado_DistanciaYVelocidadEntregaValidos_ObtieneTiempoTraslado()
		{
			//Arrange
			decimal dResultadoEsperado = 2m;
			decimal dDistancia = 20m;
			var SUT = new Tren(new MetodoEnvioDTO { dVelocidadEntrega = 10m });

			//Act
			decimal dTiempo = SUT.CalcularTiempoTraslado(dDistancia);

			//Assert
			Assert.AreEqual(dResultadoEsperado, dTiempo);
		}

		[TestMethod]
		public void CalcularTiempoTraslado_VelocidadEntregaCero_ExcepcionDivisionCero()
		{
			//Arrange
			decimal dDistancia = 20m;
			var SUT = new Tren(new MetodoEnvioDTO { dVelocidadEntrega = 0m });

			//Assert
			Assert.ThrowsException<DivideByZeroException>(() => SUT.CalcularTiempoTraslado(dDistancia));
		}

		[TestMethod]
		public void CalcularCostoEnvio_DistanciaYMargenValidos_ObtieneCosto()
		{
			//Arrange
			decimal dCostoEsperado = 52m;
			decimal dDistancia = 20m;
			decimal dMargen = 0.30m;
			var SUT = new Tren(new MetodoEnvioDTO { dCostoKilometro = 2m });

			//Act
			decimal dCosto = SUT.CalcularCostoEnvio(dDistancia, dMargen);

			//Assert
			Assert.AreEqual(dCostoEsperado, dCosto);
		}

		[TestMethod]
		public void ObtenerFechaEntrega_UnDiaEnHorasTiempoTraslado_ObtieneFechaEntregaDiaSiguiente()
		{
			//Arrange
			decimal dTiempoTraslado = 24m;
			DateTime dtEntregaEsperada = new DateTime(2020, 01, 27);
			DateTime dtPedido = new DateTime(2020, 01, 26);
			var SUT = new Tren(new MetodoEnvioDTO());

			//Act
			DateTime dtEntrega = SUT.ObtenerFechaEntrega(dtPedido, dTiempoTraslado);

			//Assert
			Assert.AreEqual(dtEntregaEsperada, dtEntrega);
		}

		[TestMethod]
		public void ObtenerFechaEntrega_ConHorasDosTiempoTraslado_ObtieneFechaEntregaConDosHorasAdicionales()
		{
			//Arrange
			decimal dTiempoTraslado = 2m;
			DateTime dtEntregaEsperada = new DateTime(2020, 01, 26, 14, 00, 00);
			DateTime dtPedido = new DateTime(2020, 01, 26, 12, 00, 00);
			var SUT = new Tren(new MetodoEnvioDTO());

			//Act
			DateTime dtEntrega = SUT.ObtenerFechaEntrega(dtPedido, dTiempoTraslado);

			//Assert
			Assert.AreEqual(dtEntregaEsperada, dtEntrega);
		}
	}
}
