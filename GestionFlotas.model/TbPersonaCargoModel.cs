using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlotas.model
{
	public class TbPersonaCargoModel
	{
		public short TbPersonaCargoId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbPersonaCargoModel()
		{
			Activo = true;
		}
	}
}
