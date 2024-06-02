using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColorActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Colors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Colors");
        }
    }
}
