using RastreoPaquetes.DTOs;
using RastreoPaquetes.Empresas;
using RastreoPaquetes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RastreoPaquetes.Creadores
{
	public class CreadorEmpresas
	{
		public List<IEmpresa> GenerarEmpresas(List<IMetodoEnvio> _lstMetodosEnvio)
		{
			List<IEmpresa> lstEmpresas = new List<IEmpresa>();

			lstEmpresas.Add(CrearEmpresaFedexBarco(_lstMetodosEnvio));
			lstEmpresas.Add(CrearEmpresaDHLAvion(_lstMetodosEnvio));
			lstEmpresas.Add(CrearEmpresaDHLBarco(_lstMetodosEnvio));
			lstEmpresas.Add(CrearEmpresaEstafetaTren(_lstMetodosEnvio));

			return lstEmpresas;
		}

		private IEmpresa CrearEmpresaFedexBarco(List<IMetodoEnvio> _lstMetodosEnvio)
		{
			IEmpresa fedexBarco;
			IMetodoEnvio envioBarco = _lstMetodosEnvio.Where(m => m.metodoEnvioDTO.cNombre.ToLower() == "barco").FirstOrDefault();
			EmpresaDTO fedexDTO = new EmpresaDTO { cNombre = "Fedex", dMargenUtilidad = 50 };

			fedexBarco = new Fedex(envioBarco, fedexDTO);

			return fedexBarco;
		}

		private IEmpresa CrearEmpresaDHLBarco(List<IMetodoEnvio> _lstMetodosEnvio)
		{
			IEmpresa dhlBarco;
			IMetodoEnvio envioBarco = _lstMetodosEnvio.Where(m => m.metodoEnvioDTO.cNombre.ToLower() == "barco").FirstOrDefault();
			EmpresaDTO dhlDTO = new EmpresaDTO { cNombre = "DHL", dMargenUtilidad = 40 };

			dhlBarco = new DHL(envioBarco, dhlDTO);

			return dhlBarco;
		}

		private IEmpresa CrearEmpresaDHLAvion(List<IMetodoEnvio> _lstMetodosEnvio)
		{
			IEmpresa dhlAvion;
			IMetodoEnvio envioAvion = _lstMetodosEnvio.Where(m => m.metodoEnvioDTO.cNombre.ToLower() == "avión").FirstOrDefault();
			EmpresaDTO dhlDTO = new EmpresaDTO { cNombre = "DHL", dMargenUtilidad = 40 };

			dhlAvion = new DHL(envioAvion, dhlDTO);

			return dhlAvion;
		}

		private IEmpresa CrearEmpresaEstafetaTren(List<IMetodoEnvio> _lstMetodosEnvio)
		{
			IEmpresa estafetaTren;
			IMetodoEnvio envioTren = _lstMetodosEnvio.Where(m => m.metodoEnvioDTO.cNombre.ToLower() == "tren").FirstOrDefault();
			EmpresaDTO estafetaDTO = new EmpresaDTO { cNombre = "Estafeta", dMargenUtilidad = 20 };

			estafetaTren = new DHL(envioTren, estafetaDTO);

			return estafetaTren;
		}
	}
}
