using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiRentalApp.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
