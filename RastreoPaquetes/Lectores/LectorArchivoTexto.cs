using RastreoPaquetes.DTOs;
using RastreoPaquetes.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RastreoPaquetes.Lectores
{
	public class LectorArchivoTexto : ILectorArchivo
	{
		public Func<string, string[]> lectorArchivo { get; set; }

		private IConvertidorPedido convertidorPedido { get; }

		public LectorArchivoTexto(IConvertidorPedido _convertidorPedido)
		{
			convertidorPedido = _convertidorPedido;
			lectorArchivo = cRuta => File.ReadAllLines(cRuta);
		}

		public List<PedidoDTO> LeerArchivoAListaPedidos(string _cRuta)
		{
			List<PedidoDTO> lstPedidos = null;
			List<string> lstLineas = null;

			lstLineas = lectorArchivo(_cRuta).ToList();
			lstPedidos = convertidorPedido.ConvertirAPedidos(lstLineas);

			return lstPedidos;
		}
	}
}
