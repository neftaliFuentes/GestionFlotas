using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbModulo
{
    public short TbModuloId { get; set; }

    public string? Nombre { get; set; }

    public string? Icono { get; set; }

    public bool Activo { get; set; }

    public bool EsPadre { get; set; }

    public short PadreId { get; set; }

    public short Orden { get; set; }

    public virtual ICollection<TbModuloAccion> TbModuloAccion { get; set; } = new List<TbModuloAccion>();
}
