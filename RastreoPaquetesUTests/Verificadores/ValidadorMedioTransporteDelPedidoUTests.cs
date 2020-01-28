using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Empresas;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.Verificadores;
using System.Collections.Generic;

namespace RastreoPaquetesUTests.Verificadores
{
	[TestClass]
	public class ValidadorMedioTransporteDelPedidoUTests
	{
		[TestMethod]
		public void RealizarValidacion_MedioDelPedidoExisteEnLasEmpresas_NoObtieneMensajeValidacion()
		{
			//Arrange
			string cResultadoEsperado = string.Empty;
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorMedioTransporteDelPedido();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = "DHL", cMedioTransporte = "Tren" } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_MedioDelPedidoNoExisteParaEmpresa_MensajeValidacionNoExisteMedioTransporte()
		{
			//Arrange
			string cMedio = "jet";
			string cResultadoEsperado = "DHL no ofrece el servicio de transporte jet, te recomendamos cotizar con otra empresa.";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorMedioTransporteDelPedido();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = "DHL", cMedioTransporte = cMedio } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_MedioDelPedidoExisteParaEmpresaYSiguienteValidacion_ObtieneMensajeSiguienteValidacion()
		{
			//Arrange
			string cResultadoEsperado = "siguiente validación";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorMedioTransporteDelPedido();
			var DOCIValidadorPedido = new Mock<IValidadorPedido>();
			DOCIValidadorPedido.Setup(v => v.RealizarValidacion(It.IsAny<List<PedidoDTO>>(), It.IsAny<List<IEmpresa>>())).Returns(cResultadoEsperado);
			SUT.AgregarSiguienteValidacion(DOCIValidadorPedido.Object);

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = "DHL", cMedioTransporte = "Tren" } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		private List<IEmpresa> ObtenerEmpresas()
		{
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvio.Setup(m => m.metodoEnvioDTO).Returns(new MetodoEnvioDTO { cNombre = "Tren" });

			lstEmpresas.Add(new DHL(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "DHL" }));
			lstEmpresas.Add(new Fedex(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "Fedex" }));

			return lstEmpresas;
		}
	}
}
