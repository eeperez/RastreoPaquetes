using RastreoPaquetes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Verificadores
{
	public class VerificadorEconomicoMismoMedio : IVerificadorCostoEconomico
	{
		public string ObtenerOpcionMasEconomica(IEmpresa _empresaPedido, List<IEmpresa> _lstEmpresas, decimal _dDistanciaPedido)
		{
			string cRespuesta = string.Empty;

			var lstEmpresasMismoMedio = ObtenerEmpresasMismoMedio(_empresaPedido, _lstEmpresas);
			if (lstEmpresasMismoMedio.Any())
			{
				decimal dCostoPedido = _empresaPedido.metodoEnvio.CalcularCostoEnvio(_dDistanciaPedido, _empresaPedido.empresaDTO.dMargenUtilidad);
				var lstEmpresasMasBaratas = (from empresa in lstEmpresasMismoMedio
											 let dDiferenciaCosto = dCostoPedido - empresa.metodoEnvio.CalcularCostoEnvio(_dDistanciaPedido, empresa.empresaDTO.dMargenUtilidad)
											 where dDiferenciaCosto > 0m
											 select new { empresa.empresaDTO.cNombre, dDiferenciaCosto }).ToList();
				if (lstEmpresasMasBaratas.Any())
				{
					var empresaMasEconomica = lstEmpresasMasBaratas.Where(e => lstEmpresasMasBaratas.Max(b => b.dDiferenciaCosto) == e.dDiferenciaCosto).FirstOrDefault();
					cRespuesta = $"Si hubieras pedido en {empresaMasEconomica.cNombre} te hubiera costado ${empresaMasEconomica.dDiferenciaCosto} más barato.";
				}
			}

			return cRespuesta;
		}

		private List<IEmpresa> ObtenerEmpresasMismoMedio(IEmpresa _empresaPedido, List<IEmpresa> _lstEmpresas)
		{
			List<IEmpresa> lstEmpresasMismoMedio = (from empresa in _lstEmpresas
													where empresa.empresaDTO.cNombre != _empresaPedido.empresaDTO.cNombre &&
													   empresa.metodoEnvio.metodoEnvioDTO.cNombre == _empresaPedido.metodoEnvio.metodoEnvioDTO.cNombre
													select empresa).ToList();

			return lstEmpresasMismoMedio;
		}
	}
}
