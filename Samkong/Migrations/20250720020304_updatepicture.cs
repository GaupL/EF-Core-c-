using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samkong.Migrations
{
    /// <inheritdoc />
    public partial class updatepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "picture",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "picture",
                table: "Employees");
        }
    }
}
