using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbTramos
{
    public short TbTramosId { get; set; }

    public string? Nombre { get; set; }

    public int Desde { get; set; }

    public int Hasta { get; set; }

    public bool Activo { get; set; }

    public string? NombreImg { get; set; }
}
