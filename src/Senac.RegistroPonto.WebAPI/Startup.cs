using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Senac.RegistroPonto.Infra.Data;
using Senac.RegistroPonto.IoC.Extensions;
using System.Reflection;

namespace Senac.RegistroPonto.WebAPI
{
    public class Startup
    {        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext(Configuration)
                .AddDependencyInjection()
                .AddMediatR(typeof(Program).Assembly);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Senac - API Registro de Ponto", Version = "v1" });
            });
        }        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContext _context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });

            _context.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Registro de Ponto API");
            });
        }
    }
}
