﻿using System.ComponentModel.DataAnnotations;

namespace GestionFlotas.model
{
	public class TbClienteModel
	{
		public int TbClienteId { get; set; }

		public int? Rut { get; set; }

		public string? Digito { get; set; }

		public string? Nombre { get; set; }
		[Required(ErrorMessage = "Debe ingresar nombre")]

		public string? RazonSocial { get; set; }
		[Required(ErrorMessage = "Debe ingresar razon social")]

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? RutFormatado { get; set; }
		public List<TbClienteSucursalModel>? MisSucursales { get; set; }
		public TbClienteModel()
		{
			Activo = true;
			ActivoString = "SI";
			MisSucursales = new List<TbClienteSucursalModel>();
		}
	}
}
