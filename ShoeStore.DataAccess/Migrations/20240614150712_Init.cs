﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "ColorShoes",
                columns: table => new
                {
                    ColorShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorShoes", x => x.ColorShoeId);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.ShoeId);
                    table.ForeignKey(
                        name: "FK_Shoes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoeImages",
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
                    table.PrimaryKey("PK_ShoeImages", x => x.ShoeImageId);
                    table.ForeignKey(
                        name: "FK_ShoeImages_ColorShoes_ColorShoeId",
                        column: x => x.ColorShoeId,
                        principalTable: "ColorShoes",
                        principalColumn: "ColorShoeId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "SpecificShoes",
                columns: table => new
                {
                    SpecificShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorShoeId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificShoes", x => x.SpecificShoeId);
                    table.ForeignKey(
                        name: "FK_SpecificShoes_ColorShoes_ColorShoeId",
                        column: x => x.ColorShoeId,
                        principalTable: "ColorShoes",
                        principalColumn: "ColorShoeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificShoes_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificShoeTests",
                columns: table => new
                {
                    SpecificShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorShoeId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Active", "CategoryName", "DateUpdated", "DisplayOrder" },
                values: new object[,]
                {
                    { 1, true, "LifeStyle", new DateOnly(1, 1, 1), 1 },
                    { 2, true, "Jordan", new DateOnly(1, 1, 1), 2 },
                    { 3, true, "Running", new DateOnly(1, 1, 1), 3 },
                    { 4, true, "Basketball", new DateOnly(1, 1, 1), 4 },
                    { 5, true, "Football", new DateOnly(1, 1, 1), 5 },
                    { 6, true, "Tranning & Gym", new DateOnly(1, 1, 1), 6 }
                });

            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ShoeId", "CategoryId", "Description", "Price", "ShoeName" },
                values: new object[,]
                {
                    { 1, 1, "Quicker than 1, 2, 3—the original hoops shoe lets you get going. This version of the AF-1 features Nike EasyOn technology for a hands-free experience. The flexible heel collapses when you step in then snaps back into place, making it easy to slip the shoe on and off. Add that to its clean, crisp leather and you've got ultimate wearability. Yeah, it's everything you love and then some.", 150m, "Nike Air Force 1 07 EasyOn" },
                    { 2, 2, "Each Craft released puts a handmade feel on the AJ1 and these low-cut sneakers are no exception. Sandy neutrals come together in kicks that beg to be a part of every outfit. Premium suede adds texture while a lightly speckled outsole grounds your look with subtle detail.", 200m, "Air Jordan 1 Low SE Craft" },
                    { 3, 3, "With maximum cushioning to support every mile, the Invincible 3 has our highest level of comfort underfoot. Its plush and bouncy ZoomX foam helps you stay stable and fresh. In other words—it's going to feel good all day, whatever you're doing. It has everything you need so you can propel down your preferred path and come back for your next run feeling ready and reinvigorated.", 250m, "Nike Invincible 3" },
                    { 4, 4, "Every challenge Zion faces—intense training, the team at the other end of the court or even gravity itself—you know he's going to rise above. This version of the Zion 3, with it's dreamy gradient fade, channels the energy that comes with every new day. And the performance tech gives you the assist you need when getting low and going high.", 170m, "Zion 3 Rising PF" },
                    { 5, 5, "We make Academy shoes for those looking to take their game to the next level. Injected with pastel pinks and crowned with a metallic Swoosh design, this special edition of the Legend 10 is inspired by what the pros wear for the world's biggest tournaments, where their brilliance takes centre stage. Add in FlyTouch Lite for amplified touch, and you have a shoe ready for personal brilliance.", 130m, "Nike Tiempo Legend 10 Academy" },
                    { 6, 6, "Whatever your why is for working out, the Metcon 9 makes it all worth it. We improved on the 8 with a larger Hyperlift plate and added rubber rope wrap. Sworn by some of the greatest athletes in the world, intended for lifters, cross-trainers and go-getters, and still the gold standard that delivers day after day.", 160m, "Nike Metcon 9 AMP" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImages_ColorShoeId",
                table: "ShoeImages",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImageTests_ColorShoeId",
                table: "ShoeImageTests",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_CategoryId",
                table: "Shoes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoes_ColorShoeId",
                table: "SpecificShoes",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoes_DiscountId",
                table: "SpecificShoes",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoeTests_ColorShoeId",
                table: "SpecificShoeTests",
                column: "ColorShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificShoeTests_DiscountId",
                table: "SpecificShoeTests",
                column: "DiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "ShoeImages");

            migrationBuilder.DropTable(
                name: "ShoeImageTests");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "SpecificShoes");

            migrationBuilder.DropTable(
                name: "SpecificShoeTests");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ColorShoes");

            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
