using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbBencinera
{
    public int TbBencineraId { get; set; }

    public short TbEmpresaBencineraId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public int TbComunaId { get; set; }

    public bool Activo { get; set; }

    public virtual TbComuna TbComuna { get; set; } = null!;

    public virtual TbBencineraEmpresa TbEmpresaBencinera { get; set; } = null!;
}
