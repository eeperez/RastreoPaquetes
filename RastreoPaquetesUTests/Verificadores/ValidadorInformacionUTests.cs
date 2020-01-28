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
	public class ValidadorInformacionUTests
	{
		[TestMethod]
		public void RealizarValidacion_ConPedidoYEmpresas_NoObtieneMensajeValidacion()
		{
			//Arrange
			string cResultadoEsperado = string.Empty;
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorInformacion();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO() }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_SinNingunaEmpresa_MensajeValidacionSinEmpresas()
		{
			//Arrange
			string cResultadoEsperado = "Es necesario tener mínimo una empresa de envíos.";
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();
			var SUT = new ValidadorInformacion();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO() }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_SinNingunPedido_MensajeValidacionSinPedido()
		{
			//Arrange
			string cResultadoEsperado = "Es necesario indicar al menos un pedido con el formato correcto.";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorInformacion();

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO>(), lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		[TestMethod]
		public void RealizarValidacion_ConPedidoYEmpresasYSiguienteValidacion_ObtieneMensajeSiguienteValidacion()
		{
			//Arrange
			string cResultadoEsperado = "siguiente validación";
			List<IEmpresa> lstEmpresas = ObtenerEmpresas();
			var SUT = new ValidadorInformacion();
			var DOCIValidadorPedido = new Mock<IValidadorPedido>();
			DOCIValidadorPedido.Setup(v => v.RealizarValidacion(It.IsAny<List<PedidoDTO>>(), It.IsAny<List<IEmpresa>>())).Returns(cResultadoEsperado);
			SUT.AgregarSiguienteValidacion(DOCIValidadorPedido.Object);

			//Act
			var cResultado = SUT.RealizarValidacion(new List<PedidoDTO> { new PedidoDTO() }, lstEmpresas);

			//Assert
			Assert.AreEqual(cResultadoEsperado, cResultado);
		}

		private List<IEmpresa> ObtenerEmpresas()
		{
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();

			lstEmpresas.Add(new DHL(DOCIMetodoEnvio.Object, new EmpresaDTO()));
			lstEmpresas.Add(new Fedex(DOCIMetodoEnvio.Object, new EmpresaDTO()));

			return lstEmpresas;
		}
	}
}
