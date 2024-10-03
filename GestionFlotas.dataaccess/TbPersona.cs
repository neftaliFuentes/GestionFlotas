using System;
using System.Collections.Generic;

namespace GestionFlotas.dataaccess;

public partial class TbPersona
{
    public int TbPersonaId { get; set; }

    public short TbPersonaCargoId { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public short TbTipoLicenciaId { get; set; }

    public short TbMunicipalidadId { get; set; }

    public string? Direccion { get; set; }

    public int TbComunaId { get; set; }

    public string? Celular { get; set; }

    public string? MaiPersonall { get; set; }

    public bool Activo { get; set; }

    public virtual TbComuna TbComuna { get; set; } = null!;

    public virtual ICollection<TbMovimiento> TbMovimiento { get; set; } = new List<TbMovimiento>();

    public virtual TbMunicipalidad TbMunicipalidad { get; set; } = null!;

    public virtual TbPersonaCargo TbPersonaCargo { get; set; } = null!;

    public virtual TbTipoLicencia TbTipoLicencia { get; set; } = null!;

    public virtual ICollection<TbUsuario> TbUsuario { get; set; } = new List<TbUsuario>();
}
