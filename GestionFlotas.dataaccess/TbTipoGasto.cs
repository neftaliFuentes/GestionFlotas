using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbTipoGasto
{
    public short TbTipoGastoId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbMovimientoGastos> TbMovimientoGastos { get; set; } = new List<TbMovimientoGastos>();
}
