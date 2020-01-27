using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Empresas;
using RastreoPaquetes.Interfaces;
using System;

namespace RastreoPaquetesUTests.Empresas
{
	[TestClass]
	public class EstafetaUTests
	{
		[TestMethod]
		public void ObtenerCosto_DatosCorrectos_ArmaCostoConTextoTuvo()
		{
			//Arrange
			string cMensajeCosto = "tuvó";
			decimal dCosto = 20m;
			string cMensajeCostoEsperado = $"{cMensajeCosto} un costo de ${dCosto.ToString()}";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvio.Setup(e => e.CalcularCostoEnvio(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(dCosto);
			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioCostoPaquete()).Returns(cMensajeCosto);

			//Act
			string cCosto = SUT.ObtenerCosto(It.IsAny<decimal>(), DOCIVerificadorEstadoPaquete.Object);

			//Assert
			Assert.AreEqual(cMensajeCostoEsperado, cCosto);
		}

		[TestMethod]
		public void CalcularRangoTiempo_DatosCorrectos_ArmaCostoConTextoTuvo()
		{
			//Arrange
			string cRangoTiempoEsperado = "2 horas";
			decimal dTiempoTraslado = 2m;
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvio.Setup(e => e.CalcularTiempoTraslado(It.IsAny<decimal>())).Returns(dTiempoTraslado);
			DOCIMetodoEnvio.Setup(e => e.ObtenerFechaEntrega(It.IsAny<DateTime>(), It.IsAny<decimal>())).Returns(It.IsAny<DateTime>());
			var DOCIRangoTiempo = new Mock<IRangoTiempo>();
			DOCIRangoTiempo.Setup(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(cRangoTiempoEsperado);

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cRangoTiempo = SUT.CalcularRangoTiempo(new PedidoDTO(), It.IsAny<DateTime>(), DOCIRangoTiempo.Object);

			//Assert
			Assert.AreEqual(cRangoTiempoEsperado, cRangoTiempo);
		}

		[TestMethod]
		public void CalcularRangoTiempo_DatosCorrectos_SoloUnaVezEjecutadoObtenerRangoTiempo()
		{
			//Arrange
			string cRangoTiempoEsperado = "2 horas";
			decimal dTiempoTraslado = 2m;
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvio.Setup(e => e.CalcularTiempoTraslado(It.IsAny<decimal>())).Returns(dTiempoTraslado);
			DOCIMetodoEnvio.Setup(e => e.ObtenerFechaEntrega(It.IsAny<DateTime>(), It.IsAny<decimal>())).Returns(It.IsAny<DateTime>());
			var DOCIRangoTiempo = new Mock<IRangoTiempo>();
			DOCIRangoTiempo.Setup(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(cRangoTiempoEsperado);

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cRangoTiempo = SUT.CalcularRangoTiempo(new PedidoDTO(), It.IsAny<DateTime>(), DOCIRangoTiempo.Object);

			//Assert
			DOCIRangoTiempo.Verify(r => r.ObtenerRangoTiempo(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
		}

		[TestMethod]
		public void ObtenerEstadoPaquete_DatosCorrectosParaArmarTexto_ObtieneTextoCuendoEstaEntregado()
		{
			//Arrange
			string cOrigen = "origen";
			string cDestino = "destino";
			string cEstadoEsperado = $"Tu paquete salió de {cOrigen} y llegó a {cDestino} hace";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteSalio()).Returns("salió");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteLlego()).Returns("llegó");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioTiempoPaquete()).Returns("hace");

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cEstado = SUT.ObtenerEstadoPaquete(new PedidoDTO { cOrigen = cOrigen, cDestino = cDestino }, DOCIVerificadorEstadoPaquete.Object);

			//Assert
			Assert.AreEqual(cEstadoEsperado, cEstado);
		}

		[TestMethod]
		public void ObtenerEstadoPaquete_DatosCorrectosParaArmarTexto_ObtieneTextoCuendoEstaPorEntregar()
		{
			//Arrange
			string cOrigen = "origen";
			string cDestino = "destino";
			string cEstadoEsperado = $"Tu paquete ha salido de {cOrigen} y llegará a {cDestino} dentro de";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteSalio()).Returns("ha salido");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteLlego()).Returns("llegará");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioTiempoPaquete()).Returns("dentro de");

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cEstado = SUT.ObtenerEstadoPaquete(new PedidoDTO { cOrigen = cOrigen, cDestino = cDestino }, DOCIVerificadorEstadoPaquete.Object);

			//Assert
			Assert.AreEqual(cEstadoEsperado, cEstado);
		}

		[TestMethod]
		public void ObtenerEstadoPaquete_DatosCorrectosParaArmarTexto_IndicaSiEjecutoUnaVezVerificarSiPaqueteSalio()
		{
			//Arrange
			string cOrigen = "origen";
			string cDestino = "destino";
			string cEstadoEsperado = $"Tu paquete ha salido de {cOrigen} y llegará a {cDestino} dentro de";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteSalio()).Returns("ha salido");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteLlego()).Returns("llegará");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioTiempoPaquete()).Returns("dentro de");

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cEstado = SUT.ObtenerEstadoPaquete(new PedidoDTO { cOrigen = cOrigen, cDestino = cDestino }, DOCIVerificadorEstadoPaquete.Object);

			//Assert spy
			DOCIVerificadorEstadoPaquete.Verify(v => v.VerificarSiPaqueteSalio(), Times.Once);
		}

		[TestMethod]
		public void ObtenerEstadoPaquete_DatosCorrectosParaArmarTexto_IndicaSiEjecutoUnaVezVerificarSiPaqueteLlego()
		{
			//Arrange
			string cOrigen = "origen";
			string cDestino = "destino";
			string cEstadoEsperado = $"Tu paquete ha salido de {cOrigen} y llegará a {cDestino} dentro de";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteSalio()).Returns("ha salido");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteLlego()).Returns("llegará");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioTiempoPaquete()).Returns("dentro de");

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cEstado = SUT.ObtenerEstadoPaquete(new PedidoDTO { cOrigen = cOrigen, cDestino = cDestino }, DOCIVerificadorEstadoPaquete.Object);

			//Assert spy
			DOCIVerificadorEstadoPaquete.Verify(v => v.VerificarSiPaqueteLlego(), Times.Once);
		}

		[TestMethod]
		public void ObtenerEstadoPaquete_DatosCorrectosParaArmarTexto_IndicaSiEjecutoUnaVezVerificarPrevioTiempoPaquete()
		{
			//Arrange
			string cOrigen = "origen";
			string cDestino = "destino";
			string cEstadoEsperado = $"Tu paquete ha salido de {cOrigen} y llegará a {cDestino} dentro de";
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			var DOCIVerificadorEstadoPaquete = new Mock<IVerificadorEstadoPaquete>();
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteSalio()).Returns("ha salido");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarSiPaqueteLlego()).Returns("llegará");
			DOCIVerificadorEstadoPaquete.Setup(v => v.VerificarPrevioTiempoPaquete()).Returns("dentro de");

			var SUT = new Estafeta(DOCIMetodoEnvio.Object, new EmpresaDTO());

			//Act
			string cEstado = SUT.ObtenerEstadoPaquete(new PedidoDTO { cOrigen = cOrigen, cDestino = cDestino }, DOCIVerificadorEstadoPaquete.Object);

			//Assert spy
			DOCIVerificadorEstadoPaquete.Verify(v => v.VerificarPrevioTiempoPaquete(), Times.Once);
		}
	}
}
