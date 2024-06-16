using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDiscountIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeImageTests");

            migrationBuilder.DropTable(
                name: "SpecificShoeTests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoeImageTests",
                columns: table => new
                {
                    ShoeImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorShoeId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeImageTests", x => x.ShoeImageId);
                    table.ForeignKey(
                        name: "FK_ShoeImageTests_ColorShoes_ColorShoeId",
                        column: x => x.ColorShoeId,
                        principalTable: "ColorShoes",
                        principalColumn: "ColorShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificShoeTests",
                columns: table => new
                {
                    SpecificShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorShoeId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificShoeTests", x => x.SpecificShoeId);
                    table.ForeignKey(
                        name: "FK_SpecificShoeTests_ColorShoes_ColorShoeId",
                        column: x => x.ColorShoeId,
                        principalTable: "ColorShoes",
                        principalColumn: "ColorShoeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificShoeTests_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImageTests_ColorShoeId",
                table: "ShoeImageTests",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoeTests_ColorShoeId",
                table: "SpecificShoeTests",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoeTests_DiscountId",
                table: "SpecificShoeTests",
                column: "DiscountId");
        }
    }
}
