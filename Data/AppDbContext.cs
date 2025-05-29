using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WEBGIS.Models;

namespace WEBGIS.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CasaPop> CasaPops { get; set; }

    public virtual DbSet<Commune> Communes { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<voirie_casa> voirie_casas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<CasaPop>(entity =>
        {
            entity.HasKey(e => e.id).HasName("CasaPop_pkey");

            entity.HasIndex(e => e.geom, "sidx_CasaPop_geom").HasMethod("gist");
        });

        modelBuilder.Entity<Commune>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Communes_pkey");

            entity.HasIndex(e => e.geom, "sidx_Communes_geom").HasMethod("gist");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.id).HasName("Site_pkey");

            entity.HasIndex(e => e.geom, "sidx_Site_geom").HasMethod("gist");
        });

        modelBuilder.Entity<voirie_casa>(entity =>
        {
            entity.HasKey(e => e.id).HasName("voirie casa_pkey");

            entity.HasIndex(e => e.geom, "sidx_voirie casa_geom").HasMethod("gist");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
