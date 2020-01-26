using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoPaquetes
{
	class Program
	{
		static void Main(string[] args)
		{
			Rastreo rastreo = new Rastreo();

			rastreo.RastrearPedidos();

			Console.ReadLine();
		}
	}
}
