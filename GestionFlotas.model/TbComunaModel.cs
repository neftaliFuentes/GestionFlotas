namespace GestionFlotas.model
{
	public class TbComunaModel
	{
		public int TbComunaId { get; set; }

		public int TbRegionId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? NombreRegion { get; set; }

		public TbComunaModel()
		{
			Activo = true;
		}
	}
}
