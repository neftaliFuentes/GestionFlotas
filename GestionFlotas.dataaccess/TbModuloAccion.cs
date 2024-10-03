using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbModuloAccion
{
    public short TbModuloAccionId { get; set; }

    public short TbModuloId { get; set; }

    public string? Nombre { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public short Orden { get; set; }

    public bool EsVisibleMenu { get; set; }

    public bool Activo { get; set; }

    public virtual TbModulo TbModulo { get; set; } = null!;

    public virtual TbPerfilModuloAccion? TbPerfilModuloAccion { get; set; }
}
