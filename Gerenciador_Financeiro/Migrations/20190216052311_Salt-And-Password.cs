using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador_Financeiro.Migrations
{
    public partial class SaltAndPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Contas_ID_CONTA",
                table: "Despesas");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Usuarios",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Senha",
                table: "Usuarios",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AlterColumn<long>(
                name: "ID_CONTA",
                table: "Despesas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Contas_ID_CONTA",
                table: "Despesas",
                column: "ID_CONTA",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Contas_ID_CONTA",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");

            migrationBuilder.AlterColumn<long>(
                name: "ID_CONTA",
                table: "Despesas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Contas_ID_CONTA",
                table: "Despesas",
                column: "ID_CONTA",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
