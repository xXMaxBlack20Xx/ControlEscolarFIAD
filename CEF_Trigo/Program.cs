using CEF_Trigo.Components;
using Datos.Contexto;
using Datos.IRepositorios;
using Datos.IRepositorios.PlanesDeEstudio;
using Datos.Repositorios;
using Datos.Repositorios.PlanesDeEstudio;
using Entidades.PerfilesDTO;
using Entidades.PerfilesDTO.PlanesDeEstudio.Carreras;
using Microsoft.EntityFrameworkCore;
using Negocios;
using Servicios.IRepositorios;
using Servicios.Repositorios;

namespace CEF_Trigo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Configuración de la cadena de conexión a la base de datos
            builder.Services.AddDbContext<ContextDB>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ContextDB"),
                    b => b.MigrationsAssembly("Datos")));

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PruebaProfile>();
                cfg.AddProfile<CarreraProfile>();
                cfg.AddProfile<Entidades.PerfilesDTO.PlanesDeEstudio.PlanEstudiosProfile>();
                cfg.AddProfile<Entidades.PerfilesDTO.PlanesDeEstudio.DocenteProfile>();
                cfg.AddProfile<Entidades.PerfilesDTO.PlanesDeEstudio.NivelAcademicoProfile>();
                cfg.AddProfile<Entidades.PerfilesDTO.PlanesDeEstudio.MateriaProfile>();
                cfg.AddProfile<Entidades.PerfilesDTO.PlanesDeEstudio.PlanEstudioMateriaProfile>();
            });

            // Agregar el repositorio al contenedor de servicios
            builder.Services.AddScoped<IPruebaRepo, PruebaRepo>();
            builder.Services.AddScoped<PruebaNegocios>();
            builder.Services.AddScoped<IPruebaServicios, PruebaServicios>();

            builder.Services.AddScoped<Negocios.Repositorios.PlanesDeEstudio.CarreraNegocios>();
            builder.Services.AddScoped<Servicios.IRepositorios.PlanesDeEstudio.ICarreraServicios, Servicios.Repositorios.PlanesDeEstudio.CarreraServicios>();
            builder.Services.AddScoped<Datos.IRepositorios.PlanesDeEstudio.ICarreraRepositorios, Datos.Repositorios.PlanesDeEstudio.CarreraRepositorio>();

            builder.Services.AddScoped<IPlanDeEstudioRepositorio, PlanEstudioRepositorio>();
            builder.Services.AddScoped<Negocios.Repositorios.PlanesDeEstudio.PlanEstudioNegocios>();
            builder.Services.AddScoped<Servicios.IRepositorios.PlanesDeEstudio.IPlanEstudioServicios, Servicios.Repositorios.PlanesDeEstudio.PlanEstudioServicios>();

            builder.Services.AddScoped<IMateriaRepositorio, MateriaRepositorio>();

            builder.Services.AddScoped<IPlanEstudioMateriaRepositorio, PlanEstudioMateriaRepositorio>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
