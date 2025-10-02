/*
    Aqui haremos la configuracion de la entidad E_Carrera con Fluent API y EF Core
*/

using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Entidades.Modelos.PlanesDeEstudio.Docentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entidades.Configuraciones.PlanesDeEstudio.Carreras;

public class CarreraConfiguration : IEntityTypeConfiguration<E_Carrera>
{
    public void Configure(EntityTypeBuilder<E_Carrera> builder)
    {
        // Configuracion para el esquema CEF de la base de datos
        builder.ToTable("Carreras", "CEF");

        // Llave primaria y AutoIncremento
        builder.HasKey(c => c.IdCarrera);
        builder.Property(c => c.IdCarrera).ValueGeneratedOnAdd();

        // Configurar propiedades requeridas y longitudes
        builder.Property(c => c.ClaveCarrera).IsRequired().HasMaxLength(3).HasColumnType("char(3)").IsFixedLength();
        // Cambie de opinion sobre la unicidad de la clave por que quiero que varios puedan tener la misma clave 
        //  Como ING o LIC, etc
        //builder.HasIndex(c => c.ClaveCarrera).IsUnique().HasDatabaseName("UK_Carreras_Clave");

        builder.Property(c => c.NombreCarrera).IsRequired().HasMaxLength(50);
        builder.HasIndex(c => c.NombreCarrera).IsUnique().HasDatabaseName("UK_Carreras_Nombre");

        builder.Property(c => c.AliasCarrera).IsRequired().HasMaxLength(50);
        builder.HasIndex(c => c.AliasCarrera).IsUnique().HasDatabaseName("UK_Carreras_Alias");

        builder.Property(c => c.EstadoCarrera).IsRequired().HasDefaultValue(true);

        // =================================================================
        // INICIO DE LA CONFIGURACIÓN DE LA RELACIÓN (SECCIÓN AÑADIDA)
        // =================================================================

        // Definición de la relación 1 a N con Docentes
        // Una Carrera TIENE UN Coordinador (que es un Docente)
        builder.HasOne(carrera => carrera.Coordinador)
               // Un Docente PUEDE TENER MUCHAS CarrerasCoordinadas
               .WithMany(docente => docente.CarrerasCoordinadas)
               // La llave foránea en la tabla Carreras es IdCoordinador
               .HasForeignKey(carrera => carrera.IdCoordinador)
               // IMPORTANTE: Evita el borrado en cascada.
               // No se podrá eliminar un Docente si es coordinador de alguna carrera.
               .OnDelete(DeleteBehavior.Restrict);
    }
}