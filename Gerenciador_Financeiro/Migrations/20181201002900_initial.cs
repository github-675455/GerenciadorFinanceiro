using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gerenciador_Financeiro.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CONTA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(maxLength: 150, nullable: true),
                    ID_USUARIO = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CONTA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DESPESA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 150, nullable: false),
                    DATA_DESPESA = table.Column<DateTime>(nullable: false),
                    ID_CONTA = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESPESA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DESPESA_CONTA_ID_CONTA",
                        column: x => x.ID_CONTA,
                        principalTable: "CONTA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RECEITA",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 150, nullable: false),
                    DATA_RECEITA = table.Column<DateTime>(nullable: false),
                    ID_CONTA = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECEITA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RECEITA_CONTA_ID_CONTA",
                        column: x => x.ID_CONTA,
                        principalTable: "CONTA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_ID_USUARIO",
                table: "CONTA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_DESPESA_ID_CONTA",
                table: "DESPESA",
                column: "ID_CONTA");

            migrationBuilder.CreateIndex(
                name: "IX_RECEITA_ID_CONTA",
                table: "RECEITA",
                column: "ID_CONTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DESPESA");

            migrationBuilder.DropTable(
                name: "RECEITA");

            migrationBuilder.DropTable(
                name: "CONTA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
