using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador_Financeiro.Migrations
{
    public partial class LoginIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios");
        }
    }
}
