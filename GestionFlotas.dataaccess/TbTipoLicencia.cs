﻿using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbTipoLicencia
{
    public short TbTipoLicenciaId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbPersona> TbPersona { get; set; } = new List<TbPersona>();
}
