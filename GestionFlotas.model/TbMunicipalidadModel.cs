using System.ComponentModel.DataAnnotations;

namespace GestionFlotas.model
{
	public class TbMunicipalidadModel
	{
		public short TbMunicipalidadId { get; set; }

		public int TbComunaId { get; set; }
		[Required(ErrorMessage = "Debe seleccionar una comuna")]

		public string Nombre { get; set; } = null!;
		[Required(ErrorMessage = "Debe ingresar nombre")]
		[StringLength(200, MinimumLength = 3, ErrorMessage = "El nombre no " +
														 "puede tener menos de 3 caracteres " +
														 "ni más de 20.")]

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? NombreComuna { get; set; }
		public string? NombreRegion { get; set; }
		public TbMunicipalidadModel()
		{
			Activo = true;
		}
	}
}
