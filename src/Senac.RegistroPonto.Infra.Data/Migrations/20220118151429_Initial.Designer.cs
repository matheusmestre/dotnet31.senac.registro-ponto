﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Senac.RegistroPonto.Infra.Data;

namespace Senac.RegistroPonto.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220118151429_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Senac.RegistroPonto.Domain.Entidades.Ponto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColaboradorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DataHora")
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("NomeColaborador")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid?>("RelacionadoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId");

                    b.HasIndex("RelacionadoId")
                        .IsUnique()
                        .HasFilter("[RelacionadoId] IS NOT NULL");

                    b.ToTable("Pontos");
                });

            modelBuilder.Entity("Senac.RegistroPonto.Domain.Entidades.Ponto", b =>
                {
                    b.HasOne("Senac.RegistroPonto.Domain.Entidades.Ponto", "Relacionado")
                        .WithOne()
                        .HasForeignKey("Senac.RegistroPonto.Domain.Entidades.Ponto", "RelacionadoId")
                        .OnDelete(DeleteBehavior.ClientNoAction);
                });
#pragma warning restore 612, 618
        }
    }
}