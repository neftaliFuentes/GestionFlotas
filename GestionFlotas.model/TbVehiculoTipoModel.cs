namespace GestionFlotas.model
{
    public class TbVehiculoTipoModel
	{
		public short TbVehiculoTipoId { get; set; }

		public string? Nombre { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
	}
}
