using RastreoPaquetes.Colores;
using RastreoPaquetes.Convertidores;
using RastreoPaquetes.Creadores;
using RastreoPaquetes.DTOs;
using RastreoPaquetes.Empresas;
using RastreoPaquetes.Interfaces;
using RastreoPaquetes.Lectores;
using RastreoPaquetes.MetodosEnvio;
using RastreoPaquetes.Verificadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes
{
	public class Rastreo
	{
		public void RastrearPedidos()
		{
			string cRutaPedidos = @"..\..\Pedidos.txt";
			string cEstadoPedido = string.Empty;
			string cRangoTiempo = string.Empty;
			string cCosto = string.Empty;
			string cRespuesta = string.Empty;
			List<IEmpresa> lstEmpresas;
			List<IMetodoEnvio> lstMetodosEnvio;
			IEmpresa empresaPedido;
			IPresentadorDatos presentador = new Presentadores.PresentadorDatos();
			DateTime dtActual = DateTime.Now;

			try
			{
				#region[Instancias de clases]

				CreadorEmpresas creadorEmpresas = new CreadorEmpresas();
				CreadorMetodosEnvio creadorMetodosEnvio = new CreadorMetodosEnvio();
				CreadorRangosTiempo creadorRangosTiempo = new CreadorRangosTiempo();
				CreadorValidaciones credorValidaciones = new CreadorValidaciones();
				IConvertidorPedido convertidorPedido = new ConvertidorPedido();
				ILectorArchivo lectorArchivo = new LectorArchivoTexto(convertidorPedido);
				CreadorValidaciones validaciones = new CreadorValidaciones();
				ContextColor contextColor = new ContextColor();
				IVerificadorEstadoPaquete verificadorEstadoPaquete = new VerificadorEstadoPaquete(dtActual);
				IVerificadorCostoEconomico verificadorEconomico = new VerificadorEconomicoMismoMedio();

				#endregion

				IRangoTiempo rangoTiempo = creadorRangosTiempo.ConfigurarRangosTiempo();
				IValidadorPedido validadorPedido = validaciones.CrearCadenaValidaciones();
				List<PedidoDTO> lstPedidos = lectorArchivo.LeerArchivoAListaPedidos(cRutaPedidos);
				lstMetodosEnvio = creadorMetodosEnvio.CrearMetodoEnvio();
				lstEmpresas = creadorEmpresas.GenerarEmpresas(lstMetodosEnvio);

				foreach (var pedido in lstPedidos)
				{
					cRespuesta = validadorPedido.RealizarValidacion(new List<PedidoDTO> { pedido }, lstEmpresas);
					if (string.IsNullOrWhiteSpace(cRespuesta))
					{
						empresaPedido = lstEmpresas.Where(e => e.empresaDTO.cNombre.ToLower() == pedido.cPaqueteria.ToLower().Trim()).FirstOrDefault();
						cEstadoPedido = empresaPedido.ObtenerEstadoPaquete(pedido, verificadorEstadoPaquete);
						cRangoTiempo = empresaPedido.CalcularRangoTiempo(pedido, dtActual, rangoTiempo);
						cCosto = empresaPedido.ObtenerCosto(pedido.dDistancia, verificadorEstadoPaquete);

						AplicarColorPorPedido(verificadorEstadoPaquete.VerificarPaqueteEntregado(), contextColor);
						cRespuesta = $"{cEstadoPedido} {cRangoTiempo} {cCosto} (cualquier reclamación con {empresaPedido.empresaDTO.cNombre}).";

						string cOpcionEconomica = verificadorEconomico.ObtenerOpcionMasEconomica(empresaPedido, lstEmpresas, pedido.dDistancia);
						if (!string.IsNullOrWhiteSpace(cOpcionEconomica))
							cRespuesta += $"\n*{cOpcionEconomica}";
					}
					else
						AplicarColorValidacion(contextColor);

					presentador.MostrarDatos(cRespuesta);
				}

			}
			catch (Exception excepcion)
			{
				presentador.MostrarDatos(excepcion.Message.Trim());
			}
		}

		private void AplicarColorPorPedido(bool _lEsPedidoEntregado, ContextColor _contextoColor)
		{
			if (_lEsPedidoEntregado)
				_contextoColor.AgregarColor(new ColorVerde());
			else
				_contextoColor.AgregarColor(new ColorAmarillo());

			_contextoColor.AplicarColorTexto();
		}

		private void AplicarColorValidacion(ContextColor _contextoColor)
		{
			_contextoColor.AgregarColor(new ColorRojo());
			_contextoColor.AplicarColorTexto();
		}
	}
}
