using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPracticasProfesionalesUtp.Data.Migrations
{
    public partial class AddStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ApellidoMat",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "Students",
                newName: "Semestre");

            migrationBuilder.RenameColumn(
                name: "ApellidoPat",
                table: "Students",
                newName: "Carrera");

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "Students",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Matricula");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Semestre",
                table: "Students",
                newName: "Nombres");

            migrationBuilder.RenameColumn(
                name: "Carrera",
                table: "Students",
                newName: "ApellidoPat");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoMat",
                table: "Students",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");
        }
    }
}
