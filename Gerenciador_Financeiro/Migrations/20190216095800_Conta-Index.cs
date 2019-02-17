using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador_Financeiro.Migrations
{
    public partial class ContaIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contas_Descricao",
                table: "Contas",
                column: "Descricao",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contas_Descricao",
                table: "Contas");
        }
    }
}
