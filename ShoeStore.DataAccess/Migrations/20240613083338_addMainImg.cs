using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addMainImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageShoeId",
                table: "ImageShoes",
                newName: "ShoeImageId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ImageShoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.RenameTable(
                name: "ImageShoes",
                newName: "ShoeImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ImageShoes");

            migrationBuilder.RenameColumn(
                name: "ShoeImageId",
                table: "ImageShoes",
                newName: "ImageShoeId");
        }
    }
}
