namespace GestionFlotas.model
{
	public class TbPaisModel
	{
		public short TbPaisId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbPaisModel()
		{
			Activo = true;
		}
	}
}
