using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlotas.model
{
	public class TbVehiculoMarcaModel
	{
		public short TbVehiculoMarcaId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbVehiculoMarcaModel()
		{
			Activo = true;
		}
	}
}
