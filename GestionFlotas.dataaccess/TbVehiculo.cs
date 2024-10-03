using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbVehiculo
{
    public int TbVehiculoId { get; set; }

    public string? Patente { get; set; }

    public short TbVehiculoTipoId { get; set; }

    public short TbVehiculoModeloId { get; set; }

    public int TbPropietarioId { get; set; }

    public string? Agno { get; set; }

    public string? Motor { get; set; }

    public string? Chasis { get; set; }

    public string? Color { get; set; }

    public DateTime? VencimientoRevTecnica { get; set; }

    public int Kilometraje { get; set; }

    public int Horometro { get; set; }

    public virtual ICollection<TbMovimiento> TbMovimiento { get; set; } = new List<TbMovimiento>();

    public virtual TbMovimientoMantencion? TbMovimientoMantencion { get; set; }

    public virtual TbPropietario TbPropietario { get; set; } = null!;

    public virtual TbVehiculoModelo TbVehiculoModelo { get; set; } = null!;

    public virtual TbVehiculoTipo TbVehiculoTipo { get; set; } = null!;
}
