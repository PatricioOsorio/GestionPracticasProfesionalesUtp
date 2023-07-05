using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Data.Migrations
{
    public partial class AddOportunidadPracticas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OportunidadPracticas",
                columns: table => new
                {
                    OportunidadPracticaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Requisitos = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OportunidadPracticas", x => x.OportunidadPracticaId);
                    table.ForeignKey(
                        name: "FK_OportunidadPracticas_Organizaciones_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizaciones",
                        principalColumn: "OrganizacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OportunidadPracticas_OrganizacionId",
                table: "OportunidadPracticas",
                column: "OrganizacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OportunidadPracticas");
        }
    }
}
