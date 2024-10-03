using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbVehiculoModelo
{
    public short TbVehiculoModeloId { get; set; }

    public short TbVehiculoMarcaId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbVehiculo> TbVehiculo { get; set; } = new List<TbVehiculo>();

    public virtual TbVehiculoMarca TbVehiculoMarca { get; set; } = null!;
}
