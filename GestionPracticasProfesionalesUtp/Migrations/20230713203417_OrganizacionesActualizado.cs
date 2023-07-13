using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Migrations
{
    public partial class OrganizacionesActualizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizaciones_CoordinadorOrganizacion_CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Organizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropColumn(
                name: "CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.AddColumn<string>(
                name: "OrganizacionId",
                table: "CoordinadorOrganizacion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinadorOrganizacion_OrganizacionId",
                table: "CoordinadorOrganizacion",
                column: "OrganizacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion",
                column: "OrganizacionId",
                principalTable: "Organizaciones",
                principalColumn: "OrganizacionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropIndex(
                name: "IX_CoordinadorOrganizacion_OrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropColumn(
                name: "OrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.AddColumn<string>(
                name: "CoordinadorOrganizacionId",
                table: "Organizaciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Organizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones",
                column: "CoordinadorOrganizacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizaciones_CoordinadorOrganizacion_CoordinadorOrganizacionId",
                table: "Organizaciones",
                column: "CoordinadorOrganizacionId",
                principalTable: "CoordinadorOrganizacion",
                principalColumn: "CoordinadorOrganizacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
