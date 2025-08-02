using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samkong.Migrations
{
    /// <inheritdoc />
    public partial class updatesortformonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Months",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Months");
        }
    }
}
