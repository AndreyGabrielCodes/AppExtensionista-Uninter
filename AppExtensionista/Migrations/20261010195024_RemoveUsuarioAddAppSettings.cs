using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExtensionista.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUsuarioAddAppSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALIMENTO_USUARIO_ID_USUARIO_CRIADOR",
                table: "ALIMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_ESTOQUE_USUARIO_ID_USUARIO_CRIADOR",
                table: "ESTOQUE");

            migrationBuilder.DropForeignKey(
                name: "FK_GUIA_ARMAZENAMENTO_USUARIO_ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRO_DESCARTE_USUARIO_ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_REGISTRO_DESCARTE_ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE");

            migrationBuilder.DropIndex(
                name: "IX_GUIA_ARMAZENAMENTO_ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO");

            migrationBuilder.DropIndex(
                name: "IX_ESTOQUE_ID_USUARIO_CRIADOR",
                table: "ESTOQUE");

            migrationBuilder.DropIndex(
                name: "IX_ALIMENTO_ID_USUARIO_CRIADOR",
                table: "ALIMENTO");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIADOR",
                table: "ALIMENTO");

            migrationBuilder.CreateTable(
                name: "APP_SETTINGS",
                columns: table => new
                {
                    ID_SETTINGS = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DIAS_ALERTA_VENCIMENTO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP_SETTINGS", x => x.ID_SETTINGS);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APP_SETTINGS");

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIADOR",
                table: "ALIMENTO",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DIAS_ALERTA_PADRAO = table.Column<int>(type: "INTEGER", nullable: false),
                    LOGIN = table.Column<string>(type: "TEXT", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", nullable: false),
                    SENHA = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRO_DESCARTE_ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE",
                column: "ID_USUARIO_RESPONSAVEL");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_ARMAZENAMENTO_ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO",
                column: "ID_USUARIO_RESPONSAVEL");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_ID_USUARIO_CRIADOR",
                table: "ESTOQUE",
                column: "ID_USUARIO_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "IX_ALIMENTO_ID_USUARIO_CRIADOR",
                table: "ALIMENTO",
                column: "ID_USUARIO_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LOGIN",
                table: "USUARIO",
                column: "LOGIN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ALIMENTO_USUARIO_ID_USUARIO_CRIADOR",
                table: "ALIMENTO",
                column: "ID_USUARIO_CRIADOR",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ESTOQUE_USUARIO_ID_USUARIO_CRIADOR",
                table: "ESTOQUE",
                column: "ID_USUARIO_CRIADOR",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GUIA_ARMAZENAMENTO_USUARIO_ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO",
                column: "ID_USUARIO_RESPONSAVEL",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRO_DESCARTE_USUARIO_ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE",
                column: "ID_USUARIO_RESPONSAVEL",
                principalTable: "USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
