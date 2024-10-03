using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMovimientoGastos
{
    public int TbMovimientoGastosId { get; set; }

    public int TbMovimientoId { get; set; }

    public short TbTipoGastoId { get; set; }

    public int Valor { get; set; }

    public string? Observacion { get; set; }

    public virtual TbMovimiento TbMovimiento { get; set; } = null!;

    public virtual TbTipoGasto TbTipoGasto { get; set; } = null!;
}
