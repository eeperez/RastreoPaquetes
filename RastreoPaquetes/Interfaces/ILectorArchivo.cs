using RastreoPaquetes.DTOs;
using System;
using System.Collections.Generic;

namespace RastreoPaquetes.Interfaces
{
	public interface ILectorArchivo
	{
		Func<string, string[]> lectorArchivo { get; set; }

		List<PedidoDTO> LeerArchivoAListaPedidos(string _cRuta);
	}
}
