using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbUsuario
{
    public int TbUsuarioId { get; set; }

    public int TbPersonaId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public short TbPerfilId { get; set; }

    public bool Activo { get; set; }

    public bool DebeCambiarPass { get; set; }

    public DateTime? FechaCaducaPass { get; set; }

    public virtual TbPerfil TbPerfil { get; set; } = null!;

    public virtual TbPersona TbPersona { get; set; } = null!;
}
