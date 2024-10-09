namespace GestionFlotas.model
{
	public class TbRegionModel
	{
		public int TbRegionId { get; set; }

		public short TbPaisId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? NombrePais { get; set; }

		public TbRegionModel()
		{
			Activo = true;
			TbPaisId = 1;
		}
	}
}
