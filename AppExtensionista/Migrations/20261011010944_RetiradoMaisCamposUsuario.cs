using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExtensionista.Migrations
{
    /// <inheritdoc />
    public partial class RetiradoMaisCamposUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIADOR",
                table: "ESTOQUE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_RESPONSAVEL",
                table: "REGISTRO_DESCARTE",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_RESPONSAVEL",
                table: "GUIA_ARMAZENAMENTO",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIADOR",
                table: "ESTOQUE",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
