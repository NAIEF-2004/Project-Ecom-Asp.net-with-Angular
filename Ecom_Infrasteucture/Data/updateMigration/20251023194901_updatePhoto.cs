using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom_Infrasteucture.Data.updateMigration
{
    /// <inheritdoc />
    public partial class updatePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Prudacts",
                newName: "OldPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                table: "Prudacts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Prudacts");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                table: "Prudacts",
                newName: "Price");
        }
    }
}
