using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Data.Migrations
{
    public partial class AddCoordinadorPracticas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoordinadorPracticas",
                columns: table => new
                {
                    CoordinadorPracticaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Facultad = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinadorPracticas", x => x.CoordinadorPracticaId);
                    table.ForeignKey(
                        name: "FK_CoordinadorPracticas_AspNetUsers_CoordinadorPracticaId",
                        column: x => x.CoordinadorPracticaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoordinadorPracticas");
        }
    }
}
