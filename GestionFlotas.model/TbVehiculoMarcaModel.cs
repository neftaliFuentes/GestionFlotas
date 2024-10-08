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
