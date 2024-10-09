namespace GestionFlotas.model
{
	public class TbBencineraModel
	{
		public int TbBencineraId { get; set; }

		public short TbEmpresaBencineraId { get; set; }

		public string? Nombre { get; set; }

		public string? Direccion { get; set; }

		public int TbComunaId { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? NombreEmpresa { get; set; }
		public string? NombreComuna { get; set; }
		public string? NombreRegion { get; set; }
		public TbBencineraModel()
		{
			Activo = true;
		}
	}
}
