using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.dataaccess;

public partial class ContextGestionFlotas : DbContext
{
    public ContextGestionFlotas(DbContextOptions<ContextGestionFlotas> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBencinera> TbBencinera { get; set; }

    public virtual DbSet<TbBencineraEmpresa> TbBencineraEmpresa { get; set; }

    public virtual DbSet<TbCliente> TbCliente { get; set; }

    public virtual DbSet<TbClienteSucursal> TbClienteSucursal { get; set; }

    public virtual DbSet<TbComuna> TbComuna { get; set; }

    public virtual DbSet<TbMantencion> TbMantencion { get; set; }

    public virtual DbSet<TbMantencionTipo> TbMantencionTipo { get; set; }

    public virtual DbSet<TbModulo> TbModulo { get; set; }

    public virtual DbSet<TbModuloAccion> TbModuloAccion { get; set; }

    public virtual DbSet<TbMovimiento> TbMovimiento { get; set; }

    public virtual DbSet<TbMovimientoDetalle> TbMovimientoDetalle { get; set; }

    public virtual DbSet<TbMovimientoGastos> TbMovimientoGastos { get; set; }

    public virtual DbSet<TbMovimientoMantencion> TbMovimientoMantencion { get; set; }

    public virtual DbSet<TbMunicipalidad> TbMunicipalidad { get; set; }

    public virtual DbSet<TbOrigenDestino> TbOrigenDestino { get; set; }

    public virtual DbSet<TbPais> TbPais { get; set; }

    public virtual DbSet<TbParametrosGenerales> TbParametrosGenerales { get; set; }

    public virtual DbSet<TbPerfil> TbPerfil { get; set; }

    public virtual DbSet<TbPerfilModuloAccion> TbPerfilModuloAccion { get; set; }

    public virtual DbSet<TbPersona> TbPersona { get; set; }

    public virtual DbSet<TbPersonaCargo> TbPersonaCargo { get; set; }

    public virtual DbSet<TbPropietario> TbPropietario { get; set; }

    public virtual DbSet<TbRegion> TbRegion { get; set; }

    public virtual DbSet<TbTaller> TbTaller { get; set; }

    public virtual DbSet<TbTipoCarga> TbTipoCarga { get; set; }

    public virtual DbSet<TbTipoGasto> TbTipoGasto { get; set; }

    public virtual DbSet<TbTipoLicencia> TbTipoLicencia { get; set; }

    public virtual DbSet<TbTramos> TbTramos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuario { get; set; }

    public virtual DbSet<TbVehiculo> TbVehiculo { get; set; }

    public virtual DbSet<TbVehiculoMarca> TbVehiculoMarca { get; set; }

    public virtual DbSet<TbVehiculoModelo> TbVehiculoModelo { get; set; }

    public virtual DbSet<TbVehiculoTipo> TbVehiculoTipo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBencinera>(entity =>
        {
            entity.HasIndex(e => e.TbEmpresaBencineraId, "IX_TbBencinera");

            entity.HasIndex(e => e.TbComunaId, "IX_TbBencinera_1");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.TbComuna).WithMany(p => p.TbBencinera)
                .HasForeignKey(d => d.TbComunaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBencinera_TbComuna");

            entity.HasOne(d => d.TbEmpresaBencinera).WithMany(p => p.TbBencinera)
                .HasForeignKey(d => d.TbEmpresaBencineraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbBencinera_TbBencineraEmpresa");
        });

        modelBuilder.Entity<TbBencineraEmpresa>(entity =>
        {
            entity.HasKey(e => e.TbBencineraEmpresaId).HasName("PK_TbEmpresaBencinera");

            entity.Property(e => e.Digito)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.Property(e => e.Digito)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbClienteSucursal>(entity =>
        {
            entity.HasIndex(e => e.TbClienteId, "IX_TbClienteSucursal");

            entity.HasIndex(e => e.TbComunaId, "IX_TbClienteSucursal_1");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.TbCliente).WithMany(p => p.TbClienteSucursal)
                .HasForeignKey(d => d.TbClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbClienteSucursal_TbCliente");

            entity.HasOne(d => d.TbComuna).WithMany(p => p.TbClienteSucursal)
                .HasForeignKey(d => d.TbComunaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbClienteSucursal_TbComuna");
        });

        modelBuilder.Entity<TbComuna>(entity =>
        {
            entity.HasIndex(e => e.TbRegionId, "IX_TbComuna");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.TbRegion).WithMany(p => p.TbComuna)
                .HasForeignKey(d => d.TbRegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComuna_TbRegion");
        });

        modelBuilder.Entity<TbMantencion>(entity =>
        {
            entity.HasIndex(e => e.TbMantencionTipoId, "IX_TbMantencion");

            entity.Property(e => e.AvisarKilometrosAvencer).HasColumnName("AvisarKilometrosAVencer");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.TbMantencionTipo).WithMany(p => p.TbMantencion)
                .HasForeignKey(d => d.TbMantencionTipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMantencion_TbMantencionTipo");
        });

        modelBuilder.Entity<TbMantencionTipo>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbModulo>(entity =>
        {
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(155)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbModuloAccion>(entity =>
        {
            entity.HasIndex(e => e.TbModuloId, "IX_TbModuloAccion");

            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(155)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.TbModulo).WithMany(p => p.TbModuloAccion)
                .HasForeignKey(d => d.TbModuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbModuloAccion_TbModulo");
        });

        modelBuilder.Entity<TbMovimiento>(entity =>
        {
            entity.HasIndex(e => e.TbVehiculoCamionId, "IX_TbMovimiento");

            entity.HasIndex(e => e.TbPersonaChoferId, "IX_TbMovimiento_1");

            entity.HasIndex(e => e.TbClienteId, "IX_TbMovimiento_2");

            entity.HasIndex(e => e.TbClienteSucursalId, "IX_TbMovimiento_3");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaViaje).HasColumnType("date");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumerosDeGuias)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TbCliente).WithMany(p => p.TbMovimiento)
                .HasForeignKey(d => d.TbClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimiento_TbCliente");

            entity.HasOne(d => d.TbClienteSucursal).WithMany(p => p.TbMovimiento)
                .HasForeignKey(d => d.TbClienteSucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimiento_TbClienteSucursal");

            entity.HasOne(d => d.TbPersonaChofer).WithMany(p => p.TbMovimiento)
                .HasForeignKey(d => d.TbPersonaChoferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimiento_TbPersona");

            entity.HasOne(d => d.TbVehiculoCamion).WithMany(p => p.TbMovimiento)
                .HasForeignKey(d => d.TbVehiculoCamionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimiento_TbVehiculo");
        });

        modelBuilder.Entity<TbMovimientoDetalle>(entity =>
        {
            entity.HasIndex(e => e.TbTipoCargaId, "IX_TbMovimientoDetalle");

            entity.HasIndex(e => e.TbMovimientoId, "IX_TbMovimientoDetalle_1");

            entity.HasIndex(e => e.TbOrigenDestinoId, "IX_TbMovimientoDetalle_2");

            entity.HasOne(d => d.TbMovimiento).WithMany(p => p.TbMovimientoDetalle)
                .HasForeignKey(d => d.TbMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoDetalle_TbMovimiento");

            entity.HasOne(d => d.TbOrigenDestino).WithMany(p => p.TbMovimientoDetalle)
                .HasForeignKey(d => d.TbOrigenDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoDetalle_TbOrigenDestino");

            entity.HasOne(d => d.TbTipoCarga).WithMany(p => p.TbMovimientoDetalle)
                .HasForeignKey(d => d.TbTipoCargaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoDetalle_TbTipoCarga");
        });

        modelBuilder.Entity<TbMovimientoGastos>(entity =>
        {
            entity.HasIndex(e => e.TbTipoGastoId, "IX_TbMovimientoGastos");

            entity.HasIndex(e => e.TbMovimientoId, "IX_TbMovimientoGastos_1");

            entity.Property(e => e.Observacion)
                .HasMaxLength(3000)
                .IsUnicode(false);

            entity.HasOne(d => d.TbMovimiento).WithMany(p => p.TbMovimientoGastos)
                .HasForeignKey(d => d.TbMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoGastos_TbMovimiento");

            entity.HasOne(d => d.TbTipoGasto).WithMany(p => p.TbMovimientoGastos)
                .HasForeignKey(d => d.TbTipoGastoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoGastos_TbTipoGasto");
        });

        modelBuilder.Entity<TbMovimientoMantencion>(entity =>
        {
            entity.HasIndex(e => e.TbVehiculoId, "IX_TbMovimientoMantencion");

            entity.HasIndex(e => e.TbMantencionId, "IX_TbMovimientoMantencion_1");

            entity.HasIndex(e => e.TbTallerId, "IX_TbMovimientoMantencion_2");

            entity.Property(e => e.TbMovimientoMantencionId).ValueGeneratedOnAdd();
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaMantencion).HasColumnType("date");

            entity.HasOne(d => d.TbMantencion).WithMany(p => p.TbMovimientoMantencion)
                .HasForeignKey(d => d.TbMantencionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoMantencion_TbMantencion");

            entity.HasOne(d => d.TbMovimientoMantencionNavigation).WithOne(p => p.TbMovimientoMantencion)
                .HasForeignKey<TbMovimientoMantencion>(d => d.TbMovimientoMantencionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoMantencion_TbVehiculo");

            entity.HasOne(d => d.TbTaller).WithMany(p => p.TbMovimientoMantencion)
                .HasForeignKey(d => d.TbTallerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbMovimientoMantencion_TbTaller");
        });

        modelBuilder.Entity<TbMunicipalidad>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbOrigenDestino>(entity =>
        {
            entity.HasIndex(e => e.TbComunaOrigenId, "IX_TbOrigenDestino");

            entity.HasIndex(e => e.TbComunaDestinoId, "IX_TbOrigenDestino_1");

            entity.HasOne(d => d.TbComunaDestino).WithMany(p => p.TbOrigenDestinoTbComunaDestino)
                .HasForeignKey(d => d.TbComunaDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbOrigenDestino_TbComuna1");

            entity.HasOne(d => d.TbComunaOrigen).WithMany(p => p.TbOrigenDestinoTbComunaOrigen)
                .HasForeignKey(d => d.TbComunaOrigenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbOrigenDestino_TbComuna");
        });

        modelBuilder.Entity<TbPais>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbParametrosGenerales>(entity =>
        {
            entity.Property(e => e.CodigoDocumento)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NombreDocumento)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbPerfil>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbPerfilModuloAccion>(entity =>
        {
            entity.HasKey(e => e.TbModuloAccionId);

            entity.HasIndex(e => e.TbPerfilId, "IX_TbPerfilModuloAccion");

            entity.Property(e => e.TbModuloAccionId).ValueGeneratedNever();
            entity.Property(e => e.TbPerfilModuloAccionId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.TbModuloAccion).WithOne(p => p.TbPerfilModuloAccion)
                .HasForeignKey<TbPerfilModuloAccion>(d => d.TbModuloAccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPerfilModuloAccion_TbModuloAccion");

            entity.HasOne(d => d.TbPerfil).WithMany(p => p.TbPerfilModuloAccion)
                .HasForeignKey(d => d.TbPerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPerfilModuloAccion_TbPerfil");
        });

        modelBuilder.Entity<TbPersona>(entity =>
        {
            entity.HasIndex(e => e.TbPersonaCargoId, "IX_TbPersona");

            entity.HasIndex(e => e.TbTipoLicenciaId, "IX_TbPersona_1");

            entity.HasIndex(e => e.TbMunicipalidadId, "IX_TbPersona_2");

            entity.HasIndex(e => e.TbComunaId, "IX_TbPersona_3");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MaiPersonall)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TbComuna).WithMany(p => p.TbPersona)
                .HasForeignKey(d => d.TbComunaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPersona_TbComuna");

            entity.HasOne(d => d.TbMunicipalidad).WithMany(p => p.TbPersona)
                .HasForeignKey(d => d.TbMunicipalidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPersona_TbMunicipalidad");

            entity.HasOne(d => d.TbPersonaCargo).WithMany(p => p.TbPersona)
                .HasForeignKey(d => d.TbPersonaCargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPersona_TbPersonaCargo");

            entity.HasOne(d => d.TbTipoLicencia).WithMany(p => p.TbPersona)
                .HasForeignKey(d => d.TbTipoLicenciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPersona_TbTipoLicencia");
        });

        modelBuilder.Entity<TbPersonaCargo>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbPropietario>(entity =>
        {
            entity.Property(e => e.Digito)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbRegion>(entity =>
        {
            entity.HasIndex(e => e.TbPaisId, "IX_TbRegion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.TbPais).WithMany(p => p.TbRegion)
                .HasForeignKey(d => d.TbPaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbRegion_TbPais");
        });

        modelBuilder.Entity<TbTaller>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(2000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTipoCarga>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTipoGasto>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTipoLicencia>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTramos>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreImg)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NombreIMG");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasIndex(e => e.TbPersonaId, "IX_TbUsuario");

            entity.HasIndex(e => e.TbPerfilId, "IX_TbUsuario_1");

            entity.Property(e => e.FechaCaducaPass).HasColumnType("date");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.TbPerfil).WithMany(p => p.TbUsuario)
                .HasForeignKey(d => d.TbPerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUsuario_TbPerfil");

            entity.HasOne(d => d.TbPersona).WithMany(p => p.TbUsuario)
                .HasForeignKey(d => d.TbPersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbUsuario_TbPersona");
        });

        modelBuilder.Entity<TbVehiculo>(entity =>
        {
            entity.HasIndex(e => e.TbVehiculoTipoId, "IX_TbVehiculo");

            entity.HasIndex(e => e.TbVehiculoModeloId, "IX_TbVehiculo_1");

            entity.HasIndex(e => e.TbPropietarioId, "IX_TbVehiculo_2");

            entity.Property(e => e.Agno)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Chasis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Motor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.VencimientoRevTecnica).HasColumnType("date");

            entity.HasOne(d => d.TbPropietario).WithMany(p => p.TbVehiculo)
                .HasForeignKey(d => d.TbPropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVehiculo_TbPropietario");

            entity.HasOne(d => d.TbVehiculoModelo).WithMany(p => p.TbVehiculo)
                .HasForeignKey(d => d.TbVehiculoModeloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVehiculo_TbVehiculoModelo");

            entity.HasOne(d => d.TbVehiculoTipo).WithMany(p => p.TbVehiculo)
                .HasForeignKey(d => d.TbVehiculoTipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVehiculo_TbVehiculoTipo");
        });

        modelBuilder.Entity<TbVehiculoMarca>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbVehiculoModelo>(entity =>
        {
            entity.HasIndex(e => e.TbVehiculoMarcaId, "IX_TbVehiculoModelo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.TbVehiculoMarca).WithMany(p => p.TbVehiculoModelo)
                .HasForeignKey(d => d.TbVehiculoMarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVehiculoModelo_TbVehiculoMarca");
        });

        modelBuilder.Entity<TbVehiculoTipo>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
