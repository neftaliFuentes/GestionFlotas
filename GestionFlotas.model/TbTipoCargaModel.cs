namespace GestionFlotas.model
{
	public class TbTipoCargaModel
	{
		public short TbTipoCargaId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbTipoCargaModel()
		{
			Activo = true;
		}
	}
}
