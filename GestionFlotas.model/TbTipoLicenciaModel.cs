namespace GestionFlotas.model
{
	public class TbTipoLicenciaModel
	{
		public short TbTipoLicenciaId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }

		public TbTipoLicenciaModel()
		{
			Activo = true;
		}
	}
}
