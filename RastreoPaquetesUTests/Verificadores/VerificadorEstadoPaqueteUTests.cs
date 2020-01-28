using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
	public class VerificadorEstadoPaqueteUTests
	{
		[TestMethod]
		public void VerificarPaqueteEntregado_FechaEntregaMenorActual_IndicaPaqueteEntregado()
		{
			//Arrange
			bool lEsperado = true;
			DateTime dtActual = new DateTime(2020, 01, 30);
			DateTime dtEntrega = new DateTime(2020, 01, 28);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var lEntregado = SUT.VerificarPaqueteEntregado();

			//Assert
			Assert.AreEqual(lEsperado, lEntregado);
		}

		[TestMethod]
		public void VerificarPaqueteEntregado_FechaEntregaMayorActual_IndicaPaqueteNoEntregado()
		{
			//Arrange
			bool lEsperado = false;
			DateTime dtActual = new DateTime(2020, 01, 28);
			DateTime dtEntrega = new DateTime(2020, 01, 30);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var lEntregado = SUT.VerificarPaqueteEntregado();

			//Assert
			Assert.AreEqual(lEsperado, lEntregado);
		}

		[TestMethod]
		public void VerificarPrevioCostoPaquete_PaqueteEntregado_ObtieneTextoTuvo()
		{
			//Arrange
			string cTextoEsperado = "tuvo";
			DateTime dtActual = new DateTime(2020, 01, 30);
			DateTime dtEntrega = new DateTime(2020, 01, 28);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarPrevioCostoPaquete();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarPrevioCostoPaquete_PaqueteNoEntregado_ObtieneTextoTendra()
		{
			//Arrange
			string cTextoEsperado = "tendrá";
			DateTime dtActual = new DateTime(2020, 01, 28);
			DateTime dtEntrega = new DateTime(2020, 01, 30);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarPrevioCostoPaquete();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarPrevioTiempoPaquete_PaqueteEntregado_ObtieneTextoHace()
		{
			//Arrange
			string cTextoEsperado = "hace";
			DateTime dtActual = new DateTime(2020, 01, 30);
			DateTime dtEntrega = new DateTime(2020, 01, 28);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarPrevioTiempoPaquete();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarPrevioTiempoPaquete_PaqueteNoEntregado_ObtieneTextoDentroDe()
		{
			//Arrange
			string cTextoEsperado = "dentro de";
			DateTime dtActual = new DateTime(2020, 01, 28);
			DateTime dtEntrega = new DateTime(2020, 01, 30);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarPrevioTiempoPaquete();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarSiPaqueteLlego_PaqueteEntregado_ObtieneTextoLlego()
		{
			//Arrange
			string cTextoEsperado = "llegó";
			DateTime dtActual = new DateTime(2020, 01, 30);
			DateTime dtEntrega = new DateTime(2020, 01, 28);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarSiPaqueteLlego();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarSiPaqueteLlego_PaqueteNoEntregado_ObtieneTextoDentroDe()
		{
			//Arrange
			string cTextoEsperado = "llegará";
			DateTime dtActual = new DateTime(2020, 01, 28);
			DateTime dtEntrega = new DateTime(2020, 01, 30);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarSiPaqueteLlego();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		//VerificarSiPaqueteSalio
		[TestMethod]
		public void VerificarSiPaqueteSalio_PaqueteEntregado_ObtieneTextoLlego()
		{
			//Arrange
			string cTextoEsperado = "salió";
			DateTime dtActual = new DateTime(2020, 01, 30);
			DateTime dtEntrega = new DateTime(2020, 01, 28);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarSiPaqueteSalio();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}

		[TestMethod]
		public void VerificarSiPaqueteSalio_PaqueteNoEntregado_ObtieneTextoDentroDe()
		{
			//Arrange
			string cTextoEsperado = "ha salido";
			DateTime dtActual = new DateTime(2020, 01, 28);
			DateTime dtEntrega = new DateTime(2020, 01, 30);
			var SUT = new VerificadorEstadoPaquete(dtActual);
			SUT.dtEntrega = dtEntrega;

			//Act
			var cTexto = SUT.VerificarSiPaqueteSalio();

			//Assert
			Assert.AreEqual(cTextoEsperado, cTexto);
		}
	}
}
