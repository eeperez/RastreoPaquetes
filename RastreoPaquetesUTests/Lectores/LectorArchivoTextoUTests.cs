using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.Lectores;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RastreoPaquetesUTests.Lectores
{
	[TestClass]
	public class LectorArchivoTextoUTests
	{
		[TestMethod]
		public void LeerArchivoAListaPedidos_RutaNoExiste_ExcepcionNoEncontroArchivo()
		{
			//Arrange
			var DOCIConvertidorPedido = new Mock<IConvertidorPedido>();
			var SUT = new LectorArchivoTexto(DOCIConvertidorPedido.Object);
			SUT.lectorArchivo = l => throw new FileNotFoundException();

			//Assert
			Assert.ThrowsException<FileNotFoundException>(() => SUT.LeerArchivoAListaPedidos("ruta inexistente"));
		}

		[TestMethod]
		public void LeerArchivoAListaPedidos_ExisteArchivo_ObtienePedido()
		{
			//Arrange
			List<PedidoDTO> lstPedidos = new List<PedidoDTO> { new PedidoDTO { cDestino = "destino" } };
			var DOCIConvertidorPedido = new Mock<IConvertidorPedido>();
			DOCIConvertidorPedido.Setup(c => c.ConvertirAPedidos(It.IsAny<List<string>>())).Returns(lstPedidos);
			var SUT = new LectorArchivoTexto(DOCIConvertidorPedido.Object);
			SUT.lectorArchivo = l => new string[] { "datos pedido" };

			//Act
			var lstPedidosLeidos = SUT.LeerArchivoAListaPedidos("ruta");

			//Assert
			Assert.IsTrue(lstPedidosLeidos.Any());
		}

		[TestMethod]
		public void LeerArchivoAListaPedidos_EjecucionConvertidorAPedidos_SeEjecutaSoloUnaVez()
		{
			//Arrange
			var DOCIConvertidorPedido = new Mock<IConvertidorPedido>();
			var SUT = new LectorArchivoTexto(DOCIConvertidorPedido.Object);
			SUT.lectorArchivo = l => new string[] { "datos pedido" };

			//Act
			var lstPedidosLeidos = SUT.LeerArchivoAListaPedidos("ruta");

			//Assert tipo spy
			DOCIConvertidorPedido.Verify(c => c.ConvertirAPedidos(It.IsAny<List<string>>()), Times.Once);
		}
	}
}
