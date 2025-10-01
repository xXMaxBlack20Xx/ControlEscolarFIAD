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

    public DbSet<E_Docentes> Docentes => Set<E_Docentes>();

    public DbSet<E_NivelAcademico> NivelesAcademicos => Set<E_NivelAcademico>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);
        mb.ApplyConfigurationsFromAssembly(typeof(ContextDB).Assembly);
    }
}