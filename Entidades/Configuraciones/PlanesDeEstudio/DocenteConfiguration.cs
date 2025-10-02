using Entidades.Modelos.PlanesDeEstudio.Docentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio;

public class DocenteConfiguration
{
    public void Configure(EntityTypeBuilder<E_Docentes> builder)
    {
        // Esquema y nombre de la tabla
        builder.ToTable("Docentes", "CEF");

        // Llave primaria y AutoIncremento
        builder.HasKey(d => d.IdDocente);
        builder.Property(d => d.IdDocente).ValueGeneratedOnAdd();

        // Configurar propiedades requeridas, longitudes e índices únicos
        builder.Property(d => d.NumeroEmpleado).IsRequired().HasMaxLength(10);
        // El número de empleado debe ser único en la tabla
        builder.HasIndex(d => d.NumeroEmpleado).IsUnique().HasDatabaseName("UK_Docentes_NumeroEmpleado");

        builder.Property(d => d.NombreDocente).IsRequired().HasMaxLength(100);
        builder.Property(d => d.PaternoDocente).IsRequired().HasMaxLength(100);
        builder.Property(d => d.MaternoDocente).IsRequired().HasMaxLength(100);

        builder.Property(d => d.EmailAlterno).IsRequired().HasMaxLength(150);
        // El email también debe ser único
        builder.HasIndex(d => d.EmailAlterno).IsUnique().HasDatabaseName("UK_Docentes_EmailAlterno");

        builder.Property(d => d.EstadoDocente).IsRequired().HasDefaultValue(true);
    }
}
