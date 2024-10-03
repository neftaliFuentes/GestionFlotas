using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbComuna
{
    public int TbComunaId { get; set; }

    public int TbRegionId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbBencinera> TbBencinera { get; set; } = new List<TbBencinera>();

    public virtual ICollection<TbClienteSucursal> TbClienteSucursal { get; set; } = new List<TbClienteSucursal>();

    public virtual ICollection<TbOrigenDestino> TbOrigenDestinoTbComunaDestino { get; set; } = new List<TbOrigenDestino>();

    public virtual ICollection<TbOrigenDestino> TbOrigenDestinoTbComunaOrigen { get; set; } = new List<TbOrigenDestino>();

    public virtual ICollection<TbPersona> TbPersona { get; set; } = new List<TbPersona>();

    public virtual TbRegion TbRegion { get; set; } = null!;
}
