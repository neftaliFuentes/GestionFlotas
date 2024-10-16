﻿using System.ComponentModel.DataAnnotations;
namespace GestionFlotas.model
{
	public class TbVehiculoMarcaModel
	{
		public short TbVehiculoMarcaId { get; set; }

		public string? Nombre { get; set; }
		[Required(ErrorMessage = "Debe ingresar nombre")]

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public List<TbVehiculoModeloModel>? MisModelos { get; set; }

		public TbVehiculoMarcaModel()
		{
			Activo = true;
			MisModelos = new List<TbVehiculoModeloModel>();
		}
	}
}
