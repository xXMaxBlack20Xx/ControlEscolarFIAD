

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio;

public class NivelAcademicoConfiguration : IEntityTypeConfiguration<E_NivelAcademico>
{
    public void Configure(EntityTypeBuilder<E_NivelAcademico> builder)
    {
        builder.ToTable("NivelesAcademicos", "CEF");

        // Primary Key
        builder.HasKey(na => na.IdNivelAcademico);

        // Properties
        builder.Property(na => na.NombreNivelAcademico)
               .IsRequired()
               .HasMaxLength(30);

        // Relationships
        builder.HasIndex(na => na.NombreNivelAcademico)
               .IsUnique()
               .HasDatabaseName("UK_NivelesAcademicos_Nombre");
    }
}