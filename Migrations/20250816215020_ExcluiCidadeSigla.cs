using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFConfin.Migrations
{
    /// <inheritdoc />
    public partial class ExcluiCidadeSigla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sigla",
                table: "Cidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sigla",
                table: "Cidade",
                type: "text",
                nullable: true);
        }
    }
}
