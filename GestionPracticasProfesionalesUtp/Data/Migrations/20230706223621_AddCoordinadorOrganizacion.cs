using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Data.Migrations
{
    public partial class AddCoordinadorOrganizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Organizaciones");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Organizaciones");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Organizaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Organizaciones",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Organizaciones",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AddColumn<string>(
                name: "CoordinadorOrganizacionId",
                table: "Organizaciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreOrganizacion",
                table: "Organizaciones",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CoordinadorOrganizaciones",
                columns: table => new
                {
                    CoordinadorOrganizacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinadorOrganizaciones", x => x.CoordinadorOrganizacionId);
                    table.ForeignKey(
                        name: "FK_CoordinadorOrganizaciones_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones",
                column: "CoordinadorOrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinadorOrganizaciones_UserId",
                table: "CoordinadorOrganizaciones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizaciones_AspNetUsers_OrganizacionId",
                table: "Organizaciones",
                column: "OrganizacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizaciones_CoordinadorOrganizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones",
                column: "CoordinadorOrganizacionId",
                principalTable: "CoordinadorOrganizaciones",
                principalColumn: "CoordinadorOrganizacionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizaciones_AspNetUsers_OrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizaciones_CoordinadorOrganizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropTable(
                name: "CoordinadorOrganizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Organizaciones_CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropColumn(
                name: "CoordinadorOrganizacionId",
                table: "Organizaciones");

            migrationBuilder.DropColumn(
                name: "NombreOrganizacion",
                table: "Organizaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Organizaciones",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Organizaciones",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Organizaciones",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Organizaciones",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Organizaciones",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");
        }
    }
}
