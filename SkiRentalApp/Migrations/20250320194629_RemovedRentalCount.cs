using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiRentalApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRentalCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalCount",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalCount",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
