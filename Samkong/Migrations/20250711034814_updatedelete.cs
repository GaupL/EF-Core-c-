using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samkong.Migrations
{
    /// <inheritdoc />
    public partial class updatedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CusId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Employees_EmpId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CusId",
                table: "Products",
                column: "CusId",
                principalTable: "Customers",
                principalColumn: "CusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Employees_EmpId",
                table: "Products",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Customers_CusId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Employees_EmpId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CusId",
                table: "Products",
                column: "CusId",
                principalTable: "Customers",
                principalColumn: "CusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Employees_EmpId",
                table: "Products",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }
    }
}
