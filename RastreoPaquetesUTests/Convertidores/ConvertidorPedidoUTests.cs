using Microsoft.VisualStudio.TestTools.UnitTesting;
using RastreoPaquetes.Convertidores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetesUTests.Convertidores
{
	[TestClass]
	public class ConvertidorPedidoUTests
	{
		[TestMethod]
		public void ConvertirAPedidos_PedidoCorrecto_ListaPedidoConDatos()
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add("origen,destino,80,paqueteria,medio,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.IsTrue(lstPedidos.Any());
		}

		[TestMethod]
		public void ConvertirAPedidos_PedidosConDosEntradas_ListaPedidoConDosPedidos()
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add("origen,destino,80,paqueteria,medio,28/01/2020");
			lstLineas.Add("origen1,destino1,80,paqueteria1,medio1,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(2, lstPedidos.Count);
		}

		[TestMethod]
		[DataRow("cozumel")]
		[DataRow("cdmx")]
		public void ConvertirAPedidos_DiferentesValoresOrigenEntradaPedido_ListaPedidoContieneMismoOrigenEnviado(string _cOrigen)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"{_cOrigen},destino,80,paqueteria,medio,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(_cOrigen, lstPedidos.FirstOrDefault().cOrigen);
		}

		[TestMethod]
		[DataRow("cozumel")]
		[DataRow("cdmx")]
		public void ConvertirAPedidos_DiferentesValoresDestinoEntradaPedido_ListaPedidoContieneMismoDestinoEnviado(string _cDestino)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,{_cDestino},80,paqueteria,medio,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(_cDestino, lstPedidos.FirstOrDefault().cDestino);
		}

		[TestMethod]
		[DataRow(80.0)]
		[DataRow(100.5)]
		public void ConvertirAPedidos_DiferentesValoresDistanciaEntradaPedido_ListaPedidoContieneMismaDistanciaEnviada(double _dDistancia)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,{_dDistancia},paqueteria,medio,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(Convert.ToDecimal(_dDistancia), lstPedidos.FirstOrDefault().dDistancia);
		}

		[TestMethod]
		[DataRow("DHL")]
		[DataRow("Fedex")]
		public void ConvertirAPedidos_DiferentesValoresPaqueteriaEntradaPedido_ListaPedidoContieneMismaPaqueteriaEnviada(string _cPaqueteria)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,80,{_cPaqueteria},medio,28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(_cPaqueteria, lstPedidos.FirstOrDefault().cPaqueteria);
		}

		[TestMethod]
		[DataRow("Tren")]
		[DataRow("Barco")]
		public void ConvertirAPedidos_DiferentesMediosTransporteEntradaPedido_ListaPedidoContieneMismoMedioTransporteEnviado(string _cMedio)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,80,paqueteria,{_cMedio},28/01/2020");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(_cMedio, lstPedidos.FirstOrDefault().cMedioTransporte);
		}

		[TestMethod]
		[DataRow("20/01/2020")]
		[DataRow("05/01/2020")]
		public void ConvertirAPedidos_DiferentesFechasEntradaPedido_ListaPedidoContieneMismasFechasEnviadas(string _cFechaPedido)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,80,paqueteria,medio,{_cFechaPedido}");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(Convert.ToDateTime(_cFechaPedido), lstPedidos.FirstOrDefault().dtPedido);
		}

		[TestMethod]
		[DataRow("origen,destino,80,paqueteria")]
		[DataRow("sin formato")]
		public void ConvertirAPedidos_PedidosEntradaSinCorrectoNumeroDatos_ListaPedidoSinElementos(string _cDatoPedido)
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"{_cDatoPedido}");
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(0, lstPedidos.Count);
		}

		[TestMethod]
		public void ConvertirAPedidos_SinDatosPedido_ListaPedidoVacia()
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			var SUT = new ConvertidorPedido();

			//Act
			var lstPedidos = SUT.ConvertirAPedidos(lstLineas);

			//Assert
			Assert.AreEqual(0, lstPedidos.Count);
		}

		[TestMethod]
		public void ConvertirAPedidos_PedidoConDistanciaNoNumerica_ExcepcionFormato()
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,distancia incorrecta,paqueteria,medio,20/01/2020");
			var SUT = new ConvertidorPedido();

			//Assert
			Assert.ThrowsException<FormatException>(() => SUT.ConvertirAPedidos(lstLineas));
		}

		[TestMethod]
		public void ConvertirAPedidos_PedidoConFormatoFechaIncorrecta_ExcepcionFormato()
		{
			//Arrange
			List<string> lstLineas = new List<string>();
			lstLineas.Add($"origen,destino,80,paqueteria,medio,Fecha incorrecta");
			var SUT = new ConvertidorPedido();

			//Assert
			Assert.ThrowsException<FormatException>(() => SUT.ConvertirAPedidos(lstLineas));
		}
	}
}
