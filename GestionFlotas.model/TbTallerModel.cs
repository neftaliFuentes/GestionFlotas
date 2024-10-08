namespace GestionFlotas.model
{
	public class TbTallerModel
	{
		public short TbTallerId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbTallerModel()
		{
			Activo = true;
		}
	}
}
