using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom_Infrasteucture.Data.updateMigration
{
    /// <inheritdoc />
    public partial class FixSpellingErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Prudacts_PrudactId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Prudacts");

            migrationBuilder.RenameColumn(
                name: "PrudactId",
                table: "Photos",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PrudactId",
                table: "Photos",
                newName: "IX_Photos_ProductId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Photos",
                newName: "PrudactId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                newName: "IX_Photos_PrudactId");

            migrationBuilder.CreateTable(
                name: "Prudacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prudacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prudacts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prudacts_CategoryId",
                table: "Prudacts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Prudacts_PrudactId",
                table: "Photos",
                column: "PrudactId",
                principalTable: "Prudacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
