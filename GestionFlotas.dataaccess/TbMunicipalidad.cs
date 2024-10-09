using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMunicipalidad
{
    public short TbMunicipalidadId { get; set; }

    public int TbComunaId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual TbComuna TbComuna { get; set; } = null!;

    public virtual ICollection<TbPersona> TbPersona { get; set; } = new List<TbPersona>();
}
