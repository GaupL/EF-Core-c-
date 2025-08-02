using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samkong.Migrations
{
    /// <inheritdoc />
    public partial class addmonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MonthId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    MonthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.MonthId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MonthId",
                table: "Products",
                column: "MonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Months_MonthId",
                table: "Products",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "MonthId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Months_MonthId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropIndex(
                name: "IX_Products_MonthId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "Products");
        }
    }
}
