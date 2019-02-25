using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador_Financeiro.Migrations
{
    public partial class CreateIndexContaAndUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Contas_ID_USUARIO_Nome",
                table: "Contas",
                columns: new[] { "ID_USUARIO", "Nome" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contas_ID_USUARIO_Nome",
                table: "Contas");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_Descricao",
                table: "Contas",
                column: "Descricao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contas_ID_USUARIO",
                table: "Contas",
                column: "ID_USUARIO");
        }
    }
}
