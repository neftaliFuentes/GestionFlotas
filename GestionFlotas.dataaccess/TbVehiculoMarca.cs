using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbVehiculoMarca
{
    public short TbVehiculoMarcaId { get; set; }

    public string? Nombre { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<TbVehiculoModelo> TbVehiculoModelo { get; set; } = new List<TbVehiculoModelo>();
}
