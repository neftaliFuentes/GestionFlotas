using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbPerfilModuloAccion
{
    public int TbPerfilModuloAccionId { get; set; }

    public short TbPerfilId { get; set; }

    public short TbModuloAccionId { get; set; }

    public virtual TbModuloAccion TbModuloAccion { get; set; } = null!;

    public virtual TbPerfil TbPerfil { get; set; } = null!;
}
