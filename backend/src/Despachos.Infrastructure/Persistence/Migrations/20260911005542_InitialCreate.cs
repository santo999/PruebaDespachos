using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Despachos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despachos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenciaExterna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RepuestoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despachos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despachos_Repuestos_RepuestoId",
                        column: x => x.RepuestoId,
                        principalTable: "Repuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Repuestos",
                columns: new[] { "Id", "CantidadDisponible", "Nombre", "Sku" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 50, "Rodamiento 62-2RS", "RPT-001" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 30, "Filtro de aceite", "RPT-002" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 20, "Correa A-60", "RPT-003" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despachos_ReferenciaExterna",
                table: "Despachos",
                column: "ReferenciaExterna",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Despachos_RepuestoId",
                table: "Despachos",
                column: "RepuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Repuestos_Sku",
                table: "Repuestos",
                column: "Sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despachos");

            migrationBuilder.DropTable(
                name: "Repuestos");
        }
    }
}
