using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbTaller
{
    public short TbTallerId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbMovimientoMantencion> TbMovimientoMantencion { get; set; } = new List<TbMovimientoMantencion>();
}
