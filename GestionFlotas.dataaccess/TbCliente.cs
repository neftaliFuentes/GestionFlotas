using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbCliente
{
    public int TbClienteId { get; set; }

    public int? Rut { get; set; }

    public string? Digito { get; set; }

    public string? Nombre { get; set; }

    public string? RazonSocial { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbClienteSucursal> TbClienteSucursal { get; set; } = new List<TbClienteSucursal>();

    public virtual ICollection<TbMovimiento> TbMovimiento { get; set; } = new List<TbMovimiento>();
}
