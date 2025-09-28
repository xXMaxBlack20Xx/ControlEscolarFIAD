using CEF_Trigo.Components;
using Datos.Contexto;
using Datos.IRepositorios;
using Datos.Repositorios;
using Entidades.PerfilesDTO;
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
            });


            // Agregar el repositorio al contenedor de servicios
            builder.Services.AddScoped<IPruebaRepo, PruebaRepo>();
            builder.Services.AddScoped<PruebaNegocios>();
            builder.Services.AddScoped<IPruebaServicios, PruebaServicios>();

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
