using Microsoft.EntityFrameworkCore;
using Senac.RegistroPonto.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senac.RegistroPonto.Infra.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions opts)
            :base(opts)
        {

        }
        public DbSet<Ponto> Pontos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
