using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TarefasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJETOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJETOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME_USUARIO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(115)", maxLength: 115, nullable: false),
                    NIVEL_ACESSO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TAREFAS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TITULO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DATA_VENCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    PRIORIDADE = table.Column<int>(type: "int", nullable: false),
                    ID_PROJETO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_USUARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TAREFAS_PROJETOS_ID_PROJETO",
                        column: x => x.ID_PROJETO,
                        principalTable: "PROJETOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAREFAS_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_PROJETO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_PROJETO = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_PROJETO", x => new { x.ID_PROJETO, x.ID_USUARIO });
                    table.ForeignKey(
                        name: "FK_USUARIO_PROJETO_PROJETOS_ID_PROJETO",
                        column: x => x.ID_PROJETO,
                        principalTable: "PROJETOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USUARIO_PROJETO_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMENTARIOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEXTO = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ID_USUARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_TAREFA = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMENTARIOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMENTARIOS_TAREFAS_ID_TAREFA",
                        column: x => x.ID_TAREFA,
                        principalTable: "TAREFAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMENTARIOS_USUARIOS_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COMENTARIOS_ID_TAREFA",
                table: "COMENTARIOS",
                column: "ID_TAREFA");

            migrationBuilder.CreateIndex(
                name: "IX_COMENTARIOS_ID_USUARIO",
                table: "COMENTARIOS",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PROJETOS_NOME",
                table: "PROJETOS",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TAREFAS_ID_PROJETO",
                table: "TAREFAS",
                column: "ID_PROJETO");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFAS_ID_USUARIO",
                table: "TAREFAS",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PROJETO_ID_USUARIO",
                table: "USUARIO_PROJETO",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMENTARIOS");

            migrationBuilder.DropTable(
                name: "USUARIO_PROJETO");

            migrationBuilder.DropTable(
                name: "TAREFAS");

            migrationBuilder.DropTable(
                name: "PROJETOS");

            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
