namespace GestionFlotas.model
{
	public class TbMantencionTipoModel
	{
		public short TbMantencionTipoId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbMantencionTipoModel()
		{
			Activo = true;
		}
	}
}
