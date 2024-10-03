using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbOrigenDestino
{
    public int TbOrigenDestinoId { get; set; }

    public int TbComunaOrigenId { get; set; }

    public int TbComunaDestinoId { get; set; }

    public int Precio { get; set; }

    public bool Activo { get; set; }

    public virtual TbComuna TbComunaDestino { get; set; } = null!;

    public virtual TbComuna TbComunaOrigen { get; set; } = null!;

    public virtual ICollection<TbMovimientoDetalle> TbMovimientoDetalle { get; set; } = new List<TbMovimientoDetalle>();
}
