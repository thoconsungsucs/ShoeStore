using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateColorShoeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ColorShoes_ColorId",
                table: "ColorShoes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorShoes_ShoeId",
                table: "ColorShoes",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorShoes_Colors_ColorId",
                table: "ColorShoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColorShoes_Shoes_ShoeId",
                table: "ColorShoes",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorShoes_Colors_ColorId",
                table: "ColorShoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ColorShoes_Shoes_ShoeId",
                table: "ColorShoes");

            migrationBuilder.DropIndex(
                name: "IX_ColorShoes_ColorId",
                table: "ColorShoes");

            migrationBuilder.DropIndex(
                name: "IX_ColorShoes_ShoeId",
                table: "ColorShoes");
        }
    }
}
