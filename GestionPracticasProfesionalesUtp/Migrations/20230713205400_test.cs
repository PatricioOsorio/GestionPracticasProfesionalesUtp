using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoordinadorOrganizacion_AspNetUsers_UserId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropIndex(
                name: "IX_CoordinadorOrganizacion_UserId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizacionId",
                table: "CoordinadorOrganizacion",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinadorOrganizacion_AspNetUsers_CoordinadorOrganizacionId",
                table: "CoordinadorOrganizacion",
                column: "CoordinadorOrganizacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion",
                column: "OrganizacionId",
                principalTable: "Organizaciones",
                principalColumn: "OrganizacionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoordinadorOrganizacion_AspNetUsers_CoordinadorOrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizacionId",
                table: "CoordinadorOrganizacion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CoordinadorOrganizacion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoordinadorOrganizacion_UserId",
                table: "CoordinadorOrganizacion",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinadorOrganizacion_AspNetUsers_UserId",
                table: "CoordinadorOrganizacion",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoordinadorOrganizacion_Organizaciones_OrganizacionId",
                table: "CoordinadorOrganizacion",
                column: "OrganizacionId",
                principalTable: "Organizaciones",
                principalColumn: "OrganizacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
