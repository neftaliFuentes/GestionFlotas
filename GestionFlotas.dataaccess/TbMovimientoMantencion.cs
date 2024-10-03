using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMovimientoMantencion
{
    public int TbMovimientoMantencionId { get; set; }

    public int TbVehiculoId { get; set; }

    public int TbMantencionId { get; set; }

    public short TbTallerId { get; set; }

    public int Folio { get; set; }

    public DateTime? FechaMantencion { get; set; }

    public int KmActual { get; set; }

    public int Valor { get; set; }

    public int KmVencimiento { get; set; }

    public int CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual TbMantencion TbMantencion { get; set; } = null!;

    public virtual TbVehiculo TbMovimientoMantencionNavigation { get; set; } = null!;

    public virtual TbTaller TbTaller { get; set; } = null!;
}
