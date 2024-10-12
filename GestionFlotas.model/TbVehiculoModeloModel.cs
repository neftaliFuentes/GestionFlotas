using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace GestionFlotas.model
{
	public class TbVehiculoModeloModel
	{
		public short TbVehiculoModeloId { get; set; }

		public short TbVehiculoMarcaId { get; set; }

		public string? Nombre { get; set; }
		[Required(ErrorMessage = "Debe ingresar nombre")]

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbVehiculoModeloModel()
		{
			Activo = true;
			ActivoString = "SI";
		}
	}
}
