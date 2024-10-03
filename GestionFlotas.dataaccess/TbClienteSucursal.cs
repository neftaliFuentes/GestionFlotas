using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbClienteSucursal
{
    public int TbClienteSucursalId { get; set; }

    public int TbClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public int TbComunaId { get; set; }

    public bool Activo { get; set; }

    public virtual TbCliente TbCliente { get; set; } = null!;

    public virtual TbComuna TbComuna { get; set; } = null!;

    public virtual ICollection<TbMovimiento> TbMovimiento { get; set; } = new List<TbMovimiento>();
}
