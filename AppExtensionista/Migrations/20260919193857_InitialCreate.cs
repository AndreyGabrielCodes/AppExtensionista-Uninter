using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExtensionista.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LOGIN = table.Column<string>(type: "TEXT", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    SENHA = table.Column<string>(type: "TEXT", nullable: false),
                    DIAS_ALERTA_PADRAO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "ALIMENTO",
                columns: table => new
                {
                    ID_ALIMENTO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_USUARIO_CRIADOR = table.Column<int>(type: "INTEGER", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    UNIDADE_MEDIDA = table.Column<int>(type: "INTEGER", nullable: false),
                    ATIVO = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALIMENTO", x => x.ID_ALIMENTO);
                    table.ForeignKey(
                        name: "FK_ALIMENTO_USUARIO_ID_USUARIO_CRIADOR",
                        column: x => x.ID_USUARIO_CRIADOR,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ESTOQUE",
                columns: table => new
                {
                    ID_ESTOQUE = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_USUARIO_CRIADOR = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_ALIMENTO = table.Column<int>(type: "INTEGER", nullable: false),
                    QUANTIDADE_ORIGINAL = table.Column<decimal>(type: "TEXT", nullable: false),
                    QUANTIDADE = table.Column<decimal>(type: "TEXT", nullable: false),
                    DATA_VALIDADE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LOCAL_ARMAZENAMENTO = table.Column<string>(type: "TEXT", nullable: false),
                    FORMA_ARMAZENAMENTO = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUE", x => x.ID_ESTOQUE);
                    table.ForeignKey(
                        name: "FK_ESTOQUE_ALIMENTO_ID_ALIMENTO",
                        column: x => x.ID_ALIMENTO,
                        principalTable: "ALIMENTO",
                        principalColumn: "ID_ALIMENTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ESTOQUE_USUARIO_ID_USUARIO_CRIADOR",
                        column: x => x.ID_USUARIO_CRIADOR,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GUIA_ARMAZENAMENTO",
                columns: table => new
                {
                    ID_GUIA_ARMAZENAMENTO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_ALIMENTO = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_USUARIO_RESPONSAVEL = table.Column<int>(type: "INTEGER", nullable: false),
                    TITULO = table.Column<string>(type: "TEXT", nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DATA_ULTIMA_ALTERACAO = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA_ARMAZENAMENTO", x => x.ID_GUIA_ARMAZENAMENTO);
                    table.ForeignKey(
                        name: "FK_GUIA_ARMAZENAMENTO_ALIMENTO_ID_ALIMENTO",
                        column: x => x.ID_ALIMENTO,
                        principalTable: "ALIMENTO",
                        principalColumn: "ID_ALIMENTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GUIA_ARMAZENAMENTO_USUARIO_ID_USUARIO_RESPONSAVEL",
                        column: x => x.ID_USUARIO_RESPONSAVEL,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRO_DESCARTE",
                columns: table => new
                {
                    ID_LISTA_DESCARTE = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_USUARIO_RESPONSAVEL = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_ESTOQUE = table.Column<int>(type: "INTEGER", nullable: false),
                    QUANTIDADE = table.Column<decimal>(type: "TEXT", nullable: false),
                    DATA_DESCARTE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DESCRICAO_MOTIVO = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRO_DESCARTE", x => x.ID_LISTA_DESCARTE);
                    table.ForeignKey(
                        name: "FK_REGISTRO_DESCARTE_ESTOQUE_ID_ESTOQUE",
                        column: x => x.ID_ESTOQUE,
                        principalTable: "ESTOQUE",
                        principalColumn: "ID_ESTOQUE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REGISTRO_DESCARTE_USUARIO_ID_USUARIO_RESPONSAVEL",
                        column: x => x.ID_USUARIO_RESPONSAVEL,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALIMENTO_ID_USUARIO_CRIADOR",
                table: "ALIMENTO",
                column: "ID_USUARIO_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_ID_ALIMENTO",
                table: "ESTOQUE",
                column: "ID_ALIMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_ID_USUARIO_CRIADOR",
                table: "ESTOQUE",
                column: "ID_USUARIO_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_ARMAZENAMENTO_ID_ALIMENTO",
                table: "GUIA_ARMAZENAMENTO",
                column: "ID_ALIMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_ARMAZENAMENTO_ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO",
                column: "ID_USUARIO_RESPONSAVEL");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_DESCARTE_ID_ESTOQUE",
                table: "REGISTRO_DESCARTE",
                column: "ID_ESTOQUE");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_DESCARTE_ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE",
                column: "ID_USUARIO_RESPONSAVEL");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LOGIN",
                table: "USUARIO",
                column: "LOGIN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GUIA_ARMAZENAMENTO");

            migrationBuilder.DropTable(
                name: "REGISTRO_DESCARTE");

            migrationBuilder.DropTable(
                name: "ESTOQUE");

            migrationBuilder.DropTable(
                name: "ALIMENTO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
