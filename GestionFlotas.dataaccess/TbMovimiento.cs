using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbMovimiento
{
    public int TbMovimientoId { get; set; }

    public string? Numero { get; set; }

    public DateTime? FechaViaje { get; set; }

    public int Folio { get; set; }

    public int TbPersonaChoferId { get; set; }

    public int TbVehiculoCamionId { get; set; }

    public int KmInicialCamion { get; set; }

    public int KmFinalCamion { get; set; }

    public int KmRecorrido { get; set; }

    public int TbVehiculoRamplaId { get; set; }

    public int HorometroInicial { get; set; }

    public int HorometroFinal { get; set; }

    public int QtyHoras { get; set; }

    public int TbClienteId { get; set; }

    public int TbClienteSucursalId { get; set; }

    public string? NumerosDeGuias { get; set; }

    public int CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual TbCliente TbCliente { get; set; } = null!;

    public virtual TbClienteSucursal TbClienteSucursal { get; set; } = null!;

    public virtual ICollection<TbMovimientoDetalle> TbMovimientoDetalle { get; set; } = new List<TbMovimientoDetalle>();

    public virtual ICollection<TbMovimientoGastos> TbMovimientoGastos { get; set; } = new List<TbMovimientoGastos>();

    public virtual TbPersona TbPersonaChofer { get; set; } = null!;

    public virtual TbVehiculo TbVehiculoCamion { get; set; } = null!;
}
