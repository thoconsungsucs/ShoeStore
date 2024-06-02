using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecificShoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageShoes_SpecificShoes_SpecificShoeId",
                table: "ImageShoes");

            migrationBuilder.DropIndex(
                name: "IX_ImageShoes_SpecificShoeId",
                table: "ImageShoes");

            migrationBuilder.DropColumn(
                name: "SpecificShoeId",
                table: "ImageShoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecificShoeId",
                table: "ImageShoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageShoes_SpecificShoeId",
                table: "ImageShoes",
                column: "SpecificShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageShoes_SpecificShoes_SpecificShoeId",
                table: "ImageShoes",
                column: "SpecificShoeId",
                principalTable: "SpecificShoes",
                principalColumn: "SpecificShoeId");
        }
    }
}
