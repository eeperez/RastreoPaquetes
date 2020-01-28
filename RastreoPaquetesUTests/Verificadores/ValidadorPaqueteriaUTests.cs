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
	public class ValidadorPaqueteriaUTests
	{
		[TestMethod]
		public void RealizarValidacion_ConPedidoExistenteEnLasEmpresas_NoObtieneMensajeValidacion()
		{
			//Arrange
			string cResultadoEsperado = string.Empty;
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorPaqueteria();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = "DHL" } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_EmpresaDelPedidoNoExisteEnEmpresas_MensajeValidacionNoExisteEmpresa()
		{
			//Arrange
			string cNombreEmpresa = "Empresa Nueva";
			string cResultadoEsperado = $"La Paquetería: {cNombreEmpresa} no se encuentra registrada en nuestra red de distribución.";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorPaqueteria();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = cNombreEmpresa } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_PedidoExistenteEnLasEmpresasYSiguienteValidacion_ObtieneMensajeSiguienteValidacion()
		{
			//Arrange
			string cResultadoEsperado = "siguiente validación";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorPaqueteria();
			var DOCIValidadorPedido = new Mock<IValidadorPedido>();
			DOCIValidadorPedido.Setup(v => v.RealizarValidacion(It.IsAny<List<PedidoDTO>>(), It.IsAny<List<IEmpresa>>())).Returns(cResultadoEsperado);
			SUT.AgregarSiguienteValidacion(DOCIValidadorPedido.Object);

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO { cPaqueteria = "DHL" } }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		private List<IEmpresa> ObtenerEmpresas()
		{
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();

			lstEmpresas.Add(new DHL(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "DHL" }));
			lstEmpresas.Add(new Fedex(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "Fedex" }));

			return lstEmpresas;
		}
	}
}
