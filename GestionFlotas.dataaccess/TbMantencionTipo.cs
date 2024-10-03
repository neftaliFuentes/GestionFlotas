using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMantencionTipo
{
    public short TbMantencionTipoId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbMantencion> TbMantencion { get; set; } = new List<TbMantencion>();
}
