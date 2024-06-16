﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoeStore.DataAccess.Data;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoeStore.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateUpdated")
                        .HasColumnType("date");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Active = true,
                            CategoryName = "LifeStyle",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 1
                        },
                        new
                        {
                            CategoryId = 2,
                            Active = true,
                            CategoryName = "Jordan",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 2
                        },
                        new
                        {
                            CategoryId = 3,
                            Active = true,
                            CategoryName = "Running",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 3
                        },
                        new
                        {
                            CategoryId = 4,
                            Active = true,
                            CategoryName = "Basketball",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 4
                        },
                        new
                        {
                            CategoryId = 5,
                            Active = true,
                            CategoryName = "Football",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 5
                        },
                        new
                        {
                            CategoryId = 6,
                            Active = true,
                            CategoryName = "Tranning & Gym",
                            DateUpdated = new DateOnly(1, 1, 1),
                            DisplayOrder = 6
                        });
                });

            modelBuilder.Entity("ShoeStore.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("DateUpdated")
                        .HasColumnType("date");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("ShoeStore.Models.ColorShoe", b =>
                {
                    b.Property<int>("ColorShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorShoeId"));

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("ColorShoeId");

                    b.ToTable("ColorShoes");
                });

            modelBuilder.Entity("ShoeStore.Models.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("DiscountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("ShoeStore.Models.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoeId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShoeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ShoeId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Shoes");

                    b.HasData(
                        new
                        {
                            ShoeId = 1,
                            CategoryId = 1,
                            Description = "Quicker than 1, 2, 3—the original hoops shoe lets you get going. This version of the AF-1 features Nike EasyOn technology for a hands-free experience. The flexible heel collapses when you step in then snaps back into place, making it easy to slip the shoe on and off. Add that to its clean, crisp leather and you've got ultimate wearability. Yeah, it's everything you love and then some.",
                            Price = 150m,
                            ShoeName = "Nike Air Force 1 07 EasyOn"
                        },
                        new
                        {
                            ShoeId = 2,
                            CategoryId = 2,
                            Description = "Each Craft released puts a handmade feel on the AJ1 and these low-cut sneakers are no exception. Sandy neutrals come together in kicks that beg to be a part of every outfit. Premium suede adds texture while a lightly speckled outsole grounds your look with subtle detail.",
                            Price = 200m,
                            ShoeName = "Air Jordan 1 Low SE Craft"
                        },
                        new
                        {
                            ShoeId = 3,
                            CategoryId = 3,
                            Description = "With maximum cushioning to support every mile, the Invincible 3 has our highest level of comfort underfoot. Its plush and bouncy ZoomX foam helps you stay stable and fresh. In other words—it's going to feel good all day, whatever you're doing. It has everything you need so you can propel down your preferred path and come back for your next run feeling ready and reinvigorated.",
                            Price = 250m,
                            ShoeName = "Nike Invincible 3"
                        },
                        new
                        {
                            ShoeId = 4,
                            CategoryId = 4,
                            Description = "Every challenge Zion faces—intense training, the team at the other end of the court or even gravity itself—you know he's going to rise above. This version of the Zion 3, with it's dreamy gradient fade, channels the energy that comes with every new day. And the performance tech gives you the assist you need when getting low and going high.",
                            Price = 170m,
                            ShoeName = "Zion 3 Rising PF"
                        },
                        new
                        {
                            ShoeId = 5,
                            CategoryId = 5,
                            Description = "We make Academy shoes for those looking to take their game to the next level. Injected with pastel pinks and crowned with a metallic Swoosh design, this special edition of the Legend 10 is inspired by what the pros wear for the world's biggest tournaments, where their brilliance takes centre stage. Add in FlyTouch Lite for amplified touch, and you have a shoe ready for personal brilliance.",
                            Price = 130m,
                            ShoeName = "Nike Tiempo Legend 10 Academy"
                        },
                        new
                        {
                            ShoeId = 6,
                            CategoryId = 6,
                            Description = "Whatever your why is for working out, the Metcon 9 makes it all worth it. We improved on the 8 with a larger Hyperlift plate and added rubber rope wrap. Sworn by some of the greatest athletes in the world, intended for lifters, cross-trainers and go-getters, and still the gold standard that delivers day after day.",
                            Price = 160m,
                            ShoeName = "Nike Metcon 9 AMP"
                        });
                });

            modelBuilder.Entity("ShoeStore.Models.ShoeImage", b =>
                {
                    b.Property<int>("ShoeImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoeImageId"));

                    b.Property<int>("ColorShoeId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.HasKey("ShoeImageId");

                    b.HasIndex("ColorShoeId");

                    b.ToTable("ShoeImages");
                });

            modelBuilder.Entity("ShoeStore.Models.SpecificShoe", b =>
                {
                    b.Property<int>("SpecificShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecificShoeId"));

                    b.Property<int>("ColorShoeId")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("SpecificShoeId");

                    b.HasIndex("ColorShoeId");

                    b.HasIndex("DiscountId");

                    b.ToTable("SpecificShoes");
                });

            modelBuilder.Entity("ShoeStore.Models.Shoe", b =>
                {
                    b.HasOne("ShoeStore.Models.Category", "Category")
                        .WithMany("Shoes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShoeStore.Models.ShoeImage", b =>
                {
                    b.HasOne("ShoeStore.Models.ColorShoe", "ColorShoe")
                        .WithMany("Images")
                        .HasForeignKey("ColorShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColorShoe");
                });

            modelBuilder.Entity("ShoeStore.Models.SpecificShoe", b =>
                {
                    b.HasOne("ShoeStore.Models.ColorShoe", "ColorShoe")
                        .WithMany()
                        .HasForeignKey("ColorShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoeStore.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColorShoe");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("ShoeStore.Models.Category", b =>
                {
                    b.Navigation("Shoes");
                });

            modelBuilder.Entity("ShoeStore.Models.ColorShoe", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
