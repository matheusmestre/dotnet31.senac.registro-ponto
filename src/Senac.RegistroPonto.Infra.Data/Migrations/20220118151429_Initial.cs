using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Senac.RegistroPonto.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeColaborador = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataHora = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    RelacionadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontos_Pontos_RelacionadoId",
                        column: x => x.RelacionadoId,
                        principalTable: "Pontos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_ColaboradorId",
                table: "Pontos",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_RelacionadoId",
                table: "Pontos",
                column: "RelacionadoId",
                unique: true,
                filter: "[RelacionadoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontos");
        }
    }
}
