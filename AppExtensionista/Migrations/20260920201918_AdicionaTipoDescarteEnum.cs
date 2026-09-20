using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppExtensionista.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTipoDescarteEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TIPO_DESCARTE",
                table: "REGISTRO_DESCARTE",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TIPO_DESCARTE",
                table: "REGISTRO_DESCARTE");
        }
    }
}
