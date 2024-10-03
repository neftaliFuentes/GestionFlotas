using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMovimientoDetalle
{
    public int TbMovimientoDetalleId { get; set; }

    public int TbMovimientoId { get; set; }

    public short TbTipoCargaId { get; set; }

    public int TbOrigenDestinoId { get; set; }

    public int Toneladas { get; set; }

    public int Valor { get; set; }

    public virtual TbMovimiento TbMovimiento { get; set; } = null!;

    public virtual TbOrigenDestino TbOrigenDestino { get; set; } = null!;

    public virtual TbTipoCarga TbTipoCarga { get; set; } = null!;
}
