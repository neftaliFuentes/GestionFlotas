namespace GestionFlotas.model
{
	public class TbTipoGastoModel
	{
		public short TbTipoGastoId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbTipoGastoModel()
		{
			Activo = true;
		}
	}
}
