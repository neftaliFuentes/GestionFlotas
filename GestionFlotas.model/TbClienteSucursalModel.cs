using System.ComponentModel.DataAnnotations;

namespace GestionFlotas.model
{
	public class TbClienteSucursalModel
	{
		public int TbClienteSucursalId { get; set; }

		public int TbClienteId { get; set; }

		public string? Nombre { get; set; }
		[Required(ErrorMessage = "Debe ingresar nombre")]

		public string? Direccion { get; set; }
		[Required(ErrorMessage = "Debe ingresar dirección")]

		public int TbComunaId { get; set; }
		[Required(ErrorMessage = "Debe seleccionar una comuna")]

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? NombreComuna { get; set; }

		public TbClienteSucursalModel()
		{
			Activo = true;
			ActivoString = "SI";
		}
	}
}
