/*
    Aqui haremos las configuracion de E_PlanDeEstudio  con Fluent API y EF Core
*/

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio;

public class PlanEstudioConfiguration : IEntityTypeConfiguration<E_PlanEstudio>
{
    public void Configure(EntityTypeBuilder<E_PlanEstudio> builder)
    {
        // Esquema y tabla
        builder.ToTable("PlanesEstudio", "CEF");

        // PK + Identity
        builder.HasKey(pe => pe.IdPlanEstudio);
        builder.Property(pe => pe.IdPlanEstudio).ValueGeneratedOnAdd();

        // Propiedades principales
        builder.Property(pe => pe.PlanEstudio).IsRequired().HasMaxLength(6).IsFixedLength();

        builder.Property(pe => pe.FechaCreacion).HasDefaultValueSql("GETDATE()");

        builder.Property(pe => pe.TotalCreditos).IsRequired();

        builder.Property(pe => pe.CreditosOptativos).IsRequired();

        builder.Property(pe => pe.CreditosObligatorios).IsRequired();

        builder.Property(pe => pe.PerfilDeIngreso).IsRequired().HasMaxLength(2048);

        builder.Property(pe => pe.PerfilDeEgreso).IsRequired().HasMaxLength(2048);

        builder.Property(pe => pe.CampoOcupacional).IsRequired().HasMaxLength(2048);

        builder.Property(pe => pe.Comentarios).HasMaxLength(2048).IsRequired(false);

        builder.Property(pe => pe.EstadoPlanEstudio).HasDefaultValue(true);

        // FK y relación (1 Carrera : N Planes)
        builder.HasOne(pe => pe.Carrera)
               .WithMany(c => c.PlanEstudios)
               .HasForeignKey(pe => pe.IdCarrera)
               .OnDelete(DeleteBehavior.Restrict);

        // Índice único: un mismo nombre de plan no debe repetirse dentro de la misma carrera
        builder.HasIndex(pe => new { pe.IdCarrera, pe.PlanEstudio })
               .IsUnique()
               .HasDatabaseName("UK_PlanesEstudio_IdCarrera_Plan");
    }
}