using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Empresas;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.Verificadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetesUTests.Verificadores
{
	[TestClass]
	public class VerificadorEconomicoMismoMedioUTests
	{
		[TestMethod]
		public void ObtenerOpcionMasEconomica_ExisteOtraEmpresaMasBarata_Mensaje20MasBarato()
		{
			//Arrange
			string cMensajeEsperado = $"Si hubieras pedido en Fedex te hubiera costado $20 más barato.";
			var lstEmpresas = ObtenerEmpresas(80m);
			var DOCIMetodoEnvioPedido = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvioPedido.Setup(m => m.metodoEnvioDTO).Returns(new MetodoEnvioDTO { cNombre = "Tren" });
			DOCIMetodoEnvioPedido.Setup(m => m.CalcularCostoEnvio(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(100m);

			IEmpresa empresaPedido = new DHL(DOCIMetodoEnvioPedido.Object, new EmpresaDTO { cNombre = "DHL" });
			var SUT = new VerificadorEconomicoMismoMedio();

			//Act
			string cMensaje = SUT.ObtenerOpcionMasEconomica(empresaPedido, lstEmpresas, It.IsAny<decimal>());

			//Assert
			Assert.AreEqual(cMensajeEsperado, cMensaje);
		}

		[TestMethod]
		public void ObtenerOpcionMasEconomica_ExisteOtraEmpresaMasCara_MensajeVacio()
		{
			//Arrange
			string cMensajeEsperado = string.Empty;
			var lstEmpresas = ObtenerEmpresas(800m);
			var DOCIMetodoEnvioPedido = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvioPedido.Setup(m => m.metodoEnvioDTO).Returns(new MetodoEnvioDTO { cNombre = "Tren" });
			DOCIMetodoEnvioPedido.Setup(m => m.CalcularCostoEnvio(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(100m);

			IEmpresa empresaPedido = new DHL(DOCIMetodoEnvioPedido.Object, new EmpresaDTO { cNombre = "DHL" });
			var SUT = new VerificadorEconomicoMismoMedio();

			//Act
			string cMensaje = SUT.ObtenerOpcionMasEconomica(empresaPedido, lstEmpresas, It.IsAny<decimal>());

			//Assert
			Assert.AreEqual(cMensajeEsperado, cMensaje);
		}

		[TestMethod]
		public void ObtenerOpcionMasEconomica_NoExisteOtraEmpresaMismoMedioTransporte_MensajeVacio()
		{
			//Arrange
			string cMensajeEsperado = string.Empty;
			var lstEmpresas = ObtenerEmpresas(80m);
			var DOCIMetodoEnvioPedido = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvioPedido.Setup(m => m.metodoEnvioDTO).Returns(new MetodoEnvioDTO { cNombre = "Barco" });
			DOCIMetodoEnvioPedido.Setup(m => m.CalcularCostoEnvio(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(100m);

			IEmpresa empresaPedido = new DHL(DOCIMetodoEnvioPedido.Object, new EmpresaDTO { cNombre = "DHL" });
			var SUT = new VerificadorEconomicoMismoMedio();

			//Act
			string cMensaje = SUT.ObtenerOpcionMasEconomica(empresaPedido, lstEmpresas, It.IsAny<decimal>());

			//Assert
			Assert.AreEqual(cMensajeEsperado, cMensaje);
		}

		private List<IEmpresa> ObtenerEmpresas(decimal _dCostoOtraEmpresa)
		{
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();
			var DOCIMetodoEnvio = new Mock<IMetodoEnvio>();
			DOCIMetodoEnvio.Setup(m => m.metodoEnvioDTO).Returns(new MetodoEnvioDTO { cNombre = "Tren" });
			DOCIMetodoEnvio.Setup(m => m.CalcularCostoEnvio(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(_dCostoOtraEmpresa);

			lstEmpresas.Add(new DHL(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "DHL" }));
			lstEmpresas.Add(new Fedex(DOCIMetodoEnvio.Object, new EmpresaDTO { cNombre = "Fedex" }));

			return lstEmpresas;
		}
	}
}
