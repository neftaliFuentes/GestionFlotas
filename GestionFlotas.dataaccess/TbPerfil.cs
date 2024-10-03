using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbPerfil
{
    public short TbPerfilId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbPerfilModuloAccion> TbPerfilModuloAccion { get; set; } = new List<TbPerfilModuloAccion>();

    public virtual ICollection<TbUsuario> TbUsuario { get; set; } = new List<TbUsuario>();
}
