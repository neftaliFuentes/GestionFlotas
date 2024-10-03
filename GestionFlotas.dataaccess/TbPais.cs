using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbPais
{
    public short TbPaisId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbRegion> TbRegion { get; set; } = new List<TbRegion>();
}
