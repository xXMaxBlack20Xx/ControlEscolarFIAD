using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio;

public class PlanEstudioMateriaConfiguration : IEntityTypeConfiguration<E_PlanEstudioMateria>
{
    public void Configure(EntityTypeBuilder<E_PlanEstudioMateria> builder)
    {
        // Esquema y tabla
        builder.ToTable("PlanEstudioMaterias", "CEF");

        // Pk + Identity
        builder.HasKey(pem => pem.IdPlanEstudioMateria);
        builder.Property(pem => pem.IdPlanEstudioMateria).ValueGeneratedOnAdd();

        // Propiedades de la tabla intermedia
        builder.Property(pem => pem.Semestre).IsRequired();
        builder.Property(pem => pem.Estado)
            .IsRequired()
            .HasDefaultValue(false);

        // Relaciones y FKs
        // Relacion con E_PlanEstudio (1 Plan : N PlanEstudioMateria)
        builder.HasOne(pem => pem.PlanEstudio)
               .WithMany(pe => pe.PlanEstudioMaterias)
               .HasForeignKey(pem => pem.IdPlanEstudio)
               .OnDelete(DeleteBehavior.Restrict);

        // Relacion con E_Materias (1 Materia : N PlanEstudioMateria)
        builder.HasOne(pem => pem.Materia)
               .WithMany(m => m.PlanesEstudioMaterias)
               .HasForeignKey(pem => pem.IdMateria)
               .OnDelete(DeleteBehavior.Restrict);

        // Indice único para evitar duplicados de la misma materia en un plan de estudio
        builder.HasIndex(pem => new { pem.IdPlanEstudio, pem.IdMateria })
               .IsUnique()
               .HasDatabaseName("UK_PlanEstudioMaterias_Plan_Materia");
    }
}