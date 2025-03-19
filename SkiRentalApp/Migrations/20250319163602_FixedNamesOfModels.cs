using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiRentalApp.Migrations
{
    /// <inheritdoc />
    public partial class FixedNamesOfModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemStatuses",
                newName: "StatusName");

            migrationBuilder.RenameIndex(
                name: "IX_ItemStatuses_Name",
                table: "ItemStatuses",
                newName: "IX_ItemStatuses_StatusName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Items",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                newName: "IX_Categories_CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "ItemStatuses",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_ItemStatuses_StatusName",
                table: "ItemStatuses",
                newName: "IX_ItemStatuses_Name");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Items",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                newName: "IX_Categories_Name");
        }
    }
}
