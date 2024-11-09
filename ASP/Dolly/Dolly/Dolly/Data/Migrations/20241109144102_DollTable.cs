using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dolly.Data.Migrations
{
    /// <inheritdoc />
    public partial class DollTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Dolls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Dolls");
        }
    }
}
