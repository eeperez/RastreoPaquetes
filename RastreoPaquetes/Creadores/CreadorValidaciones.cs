using RastreoPaquetes.Interfaces;
using RastreoPaquetes.Verificadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes.Creadores
{
	public class CreadorValidaciones
	{
		public IValidadorPedido CrearCadenaValidaciones()
		{
			IValidadorPedido validadorInfo = new ValidadorInformacion();
			IValidadorPedido validadorPaqueteria = new ValidadorPaqueteria();
			IValidadorPedido validadorMedioTransporteDelPedido = new ValidadorMedioTransporteDelPedido();

			validadorInfo.AgregarSiguienteValidacion(validadorPaqueteria);
			validadorPaqueteria.AgregarSiguienteValidacion(validadorMedioTransporteDelPedido);

			return validadorInfo;
		}
	}
}
