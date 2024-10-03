using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMantencion
{
    public int TbMantencionId { get; set; }

    public short TbMantencionTipoId { get; set; }

    public string? Descripcion { get; set; }

    public int DuracionEnKilometros { get; set; }

    public int AvisarKilometrosAvencer { get; set; }

    public virtual TbMantencionTipo TbMantencionTipo { get; set; } = null!;

    public virtual ICollection<TbMovimientoMantencion> TbMovimientoMantencion { get; set; } = new List<TbMovimientoMantencion>();
}
