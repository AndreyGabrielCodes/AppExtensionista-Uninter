using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExtensionista.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEstoqueAdicionaMarcaRemoveLocais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FORMA_ARMAZENAMENTO",
                table: "ESTOQUE");

            migrationBuilder.RenameColumn(
                name: "LOCAL_ARMAZENAMENTO",
                table: "ESTOQUE",
                newName: "MARCA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MARCA",
                table: "ESTOQUE",
                newName: "LOCAL_ARMAZENAMENTO");

            migrationBuilder.AddColumn<string>(
                name: "FORMA_ARMAZENAMENTO",
                table: "ESTOQUE",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
