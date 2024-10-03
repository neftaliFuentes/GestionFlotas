using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbRegion
{
    public int TbRegionId { get; set; }

    public short TbPaisId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbComuna> TbComuna { get; set; } = new List<TbComuna>();

    public virtual TbPais TbPais { get; set; } = null!;
}
