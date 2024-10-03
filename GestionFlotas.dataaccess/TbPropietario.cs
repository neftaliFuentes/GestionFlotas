using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbPropietario
{
    public int TbPropietarioId { get; set; }

    public int? Rut { get; set; }

    public string? Digito { get; set; }

    public string? Nombre { get; set; }

    public string? RazonSocial { get; set; }

    public virtual ICollection<TbVehiculo> TbVehiculo { get; set; } = new List<TbVehiculo>();
}
