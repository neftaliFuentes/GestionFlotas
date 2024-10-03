using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbBencineraEmpresa
{
    public short TbBencineraEmpresaId { get; set; }

    public int Rut { get; set; }

    public string? Digito { get; set; }

    public string? Nombre { get; set; }

    public string? RazonSocial { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbBencinera> TbBencinera { get; set; } = new List<TbBencinera>();
}
