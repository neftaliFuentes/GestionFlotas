using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbParametrosGenerales
{
    public short TbParametrosGeneralesId { get; set; }

    public string? NombreDocumento { get; set; }

    public string? CodigoDocumento { get; set; }
}
