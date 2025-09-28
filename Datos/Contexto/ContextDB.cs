using Entidades.Configuraciones.PlanesDeEstudio;
using Entidades.Modelos;
using Entidades.Modelos.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contexto;

public class ContextDB(DbContextOptions<ContextDB> opt) : DbContext(opt)
{
    // DbSets
    public DbSet<E_Prueba> Pruebas => Set<E_Prueba>();

    public DbSet<E_Carrera> Carreras => Set<E_Carrera>();

    public DbSet<E_PlanEstudio> PlanEstudios => Set<E_PlanEstudio>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<E_Prueba>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(80);
        });
        
        mb.ApplyConfiguration(new CarreraConfiguration());
        mb.ApplyConfiguration(new PlanEstudioConfiguration());

        base.OnModelCreating(mb);
    }
}