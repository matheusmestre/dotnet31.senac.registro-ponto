using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senac.RegistroPonto.Domain.Entidades;

namespace Senac.RegistroPonto.Infra.Data.Configurations
{
    public class PontoConfig : IEntityTypeConfiguration<Ponto>
    {
        public void Configure(EntityTypeBuilder<Ponto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ColaboradorId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.NomeColaborador)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(x => x.DataHora)
                .HasColumnType("datetimeoffset(0)")
                .IsRequired();

            builder.Property(x => x.Tipo)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.RelacionadoId)
                .HasColumnType("uniqueidentifier")
                .IsRequired(false);

            builder
                .HasOne(x => x.Relacionado)
                .WithOne()
                .HasPrincipalKey<Ponto>(x => x.Id)
                .HasForeignKey<Ponto>(x => x.RelacionadoId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .IsRequired(false);     
            
            builder.HasIndex(x => x.ColaboradorId);

            builder.ToTable("Pontos");
        }
    }
}
