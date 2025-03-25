using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontuacaoRealTime.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarConstraintUnicaConsumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PessoaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataConsumo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Saldo = table.Column<int>(type: "INTEGER", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.PessoaId);
                });

            migrationBuilder.CreateTable(
                name: "Memorial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsumoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorPontuavel = table.Column<decimal>(type: "TEXT", nullable: false),
                    PontosObtidos = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memorial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memorial_Consumos_ConsumoId",
                        column: x => x.ConsumoId,
                        principalTable: "Consumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_PessoaId_DataConsumo_ValorTotal",
                table: "Consumos",
                columns: new[] { "PessoaId", "DataConsumo", "ValorTotal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memorial_ConsumoId",
                table: "Memorial",
                column: "ConsumoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memorial");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Consumos");
        }
    }
}
