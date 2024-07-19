using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addBagModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    BagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecificShoeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.BagId);
                    table.ForeignKey(
                        name: "FK_Bags_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bags_SpecificShoes_SpecificShoeId",
                        column: x => x.SpecificShoeId,
                        principalTable: "SpecificShoes",
                        principalColumn: "SpecificShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_ApplicationUserId",
                table: "Bags",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bags_SpecificShoeId",
                table: "Bags",
                column: "SpecificShoeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bags");
        }
    }
}
