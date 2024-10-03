using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbTipoCarga
{
    public short TbTipoCargaId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbMovimientoDetalle> TbMovimientoDetalle { get; set; } = new List<TbMovimientoDetalle>();
}
