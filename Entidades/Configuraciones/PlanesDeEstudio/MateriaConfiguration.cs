using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio;

public class MateriaConfiguration : IEntityTypeConfiguration<E_Materias>
{
    public void Configure(EntityTypeBuilder<E_Materias> builder)
    {
        // Esquema y tabla
        builder.ToTable("Materias", "CEF");

        // PK + Identity
        builder.HasKey(m => m.IdMateria);
        builder.Property(m => m.IdMateria).ValueGeneratedOnAdd();

        // Propiedades y restricciones
        builder.Property(m => m.ClaveMateria)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(m => m.NombreMateria)
            .IsRequired()
            .HasMaxLength(100);

        // Campos numericos requeridos
        builder.Property(m => m.HC).IsRequired();
        builder.Property(m => m.HL).IsRequired();
        builder.Property(m => m.HT).IsRequired();
        builder.Property(m => m.HPC).IsRequired();
        builder.Property(m => m.HCL).IsRequired();
        builder.Property(m => m.HE).IsRequired();
        builder.Property(m => m.CR).IsRequired();

        // Campos string requeridos con max length
        builder.Property(m => m.PropositoGeneral).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Competencia).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Evidencia).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Metodologia).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Criterios).IsRequired().HasMaxLength(200);
        builder.Property(m => m.BibliografiaBasica).IsRequired().HasMaxLength(200);
        builder.Property(m => m.BibliografiaComplementaria).IsRequired().HasMaxLength(200);
        builder.Property(m => m.PerfilDocente).IsRequired().HasMaxLength(200);

        builder.Property(m => m.PathPUA)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(m => m.EstadoMateria).HasDefaultValue(true);

        // Índice único en ClaveMateria
        builder.HasIndex(m => m.ClaveMateria)
               .IsUnique()
               .HasDatabaseName("UK_Materias_ClaveMateria");
    }

}