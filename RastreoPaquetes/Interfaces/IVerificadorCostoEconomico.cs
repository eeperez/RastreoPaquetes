using System.Collections.Generic;

namespace RastreoPaquetes.Interfaces
{
	public interface IVerificadorCostoEconomico
	{
		string ObtenerOpcionMasEconomica(IEmpresa _empresaPedido, List<IEmpresa> _lstEmpresas, decimal _dDistanciaPedido);
	}
}
