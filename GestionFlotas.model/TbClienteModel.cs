namespace GestionFlotas.model
{
	public class TbClienteModel
	{
		public int TbClienteId { get; set; }

		public int? Rut { get; set; }

		public string? Digito { get; set; }

		public string? Nombre { get; set; }

		public string? RazonSocial { get; set; }

		public bool Activo { get; set; }

		// Variables Virtuales
		public string? ActivoString { get; set; }
		public string? RutFormatado { get; set; }
		public TbClienteModel()
		{
			Activo = true;
		}
	}
}
