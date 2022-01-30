using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senac.RegistroPonto.Domain.Interfaces;
using Senac.RegistroPonto.DomainServices.Services;
using Senac.RegistroPonto.Infra.Data;
using Senac.RegistroPonto.Infra.Data.Repositories;

namespace Senac.RegistroPonto.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ApplicationContext>();
            services.AddScoped<DbContext, ApplicationContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Domain Services
            services.AddScoped<IPontoDomainService, PontoDomainService>();

            //Repositories
            services.AddScoped<IPontoRepository, PontoRepository>();

            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("ApplicationContext");
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(connString));

            return services;
        }
    }
}
