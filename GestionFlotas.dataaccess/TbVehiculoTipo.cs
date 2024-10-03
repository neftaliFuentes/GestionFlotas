using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbVehiculoTipo
{
    public short TbVehiculoTipoId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbVehiculo> TbVehiculo { get; set; } = new List<TbVehiculo>();
}
