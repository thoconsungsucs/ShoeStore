using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;

namespace ShoeStore.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ShoeImage> ShoeImages { get; set; }
        /*        DbSet<Size> Sizes { get; set; }
                DbSet<Gender> Genders { get; set; }*/
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<SpecificShoe> SpecificShoes { get; set; }
        public DbSet<ColorShoe> ColorShoes { get; set; }
        public DbSet<ShoeImageTest> ShoeImageTests { get; set; }
        public DbSet<SpecificShoeTest> SpecificShoeTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "LifeStyle", DisplayOrder = 1, Active = true },
                new Category { CategoryId = 2, CategoryName = "Jordan", DisplayOrder = 2, Active = true },
                new Category { CategoryId = 3, CategoryName = "Running", DisplayOrder = 3, Active = true },
                new Category { CategoryId = 4, CategoryName = "Basketball", DisplayOrder = 4, Active = true },
                new Category { CategoryId = 5, CategoryName = "Football", DisplayOrder = 5, Active = true },
                new Category { CategoryId = 6, CategoryName = "Tranning & Gym", DisplayOrder = 6, Active = true }
                            );

            modelBuilder.Entity<Shoe>().HasData(
                new Shoe
                {
                    ShoeId = 1,
                    ShoeName = "Nike Air Force 1 07 EasyOn",
                    CategoryId = 1,
                    Description = "Quicker than 1, 2, 3—the original hoops shoe lets you get going. This version of the AF-1 features Nike EasyOn technology for a hands-free experience. The flexible heel collapses when you step in then snaps back into place, making it easy to slip the shoe on and off. Add that to its clean, crisp leather and you've got ultimate wearability. Yeah, it's everything you love and then some.",
                    Price = 150,
                },
                new Shoe
                {
                    ShoeId = 2,
                    ShoeName = "Air Jordan 1 Low SE Craft",
                    CategoryId = 2,
                    Description = "Each Craft released puts a handmade feel on the AJ1 and these low-cut sneakers are no exception. Sandy neutrals come together in kicks that beg to be a part of every outfit. Premium suede adds texture while a lightly speckled outsole grounds your look with subtle detail.",
                    Price = 200,
                },
                new Shoe
                {
                    ShoeId = 3,
                    ShoeName = "Nike Invincible 3",
                    CategoryId = 3,
                    Description = "With maximum cushioning to support every mile, the Invincible 3 has our highest level of comfort underfoot. Its plush and bouncy ZoomX foam helps you stay stable and fresh. In other words—it's going to feel good all day, whatever you're doing. It has everything you need so you can propel down your preferred path and come back for your next run feeling ready and reinvigorated.",
                    Price = 250,
                },
                new Shoe
                {
                    ShoeId = 4,
                    ShoeName = "Zion 3 Rising PF",
                    CategoryId = 4,
                    Description = "Every challenge Zion faces—intense training, the team at the other end of the court or even gravity itself—you know he's going to rise above. This version of the Zion 3, with it's dreamy gradient fade, channels the energy that comes with every new day. And the performance tech gives you the assist you need when getting low and going high.",
                    Price = 170,
                },
                new Shoe
                {
                    ShoeId = 5,
                    ShoeName = "Nike Tiempo Legend 10 Academy",
                    CategoryId = 5,
                    Description = "We make Academy shoes for those looking to take their game to the next level. Injected with pastel pinks and crowned with a metallic Swoosh design, this special edition of the Legend 10 is inspired by what the pros wear for the world's biggest tournaments, where their brilliance takes centre stage. Add in FlyTouch Lite for amplified touch, and you have a shoe ready for personal brilliance.",
                    Price = 130,
                },
                new Shoe
                {
                    ShoeId = 6,
                    ShoeName = "Nike Metcon 9 AMP",
                    CategoryId = 6,
                    Description = "Whatever your why is for working out, the Metcon 9 makes it all worth it. We improved on the 8 with a larger Hyperlift plate and added rubber rope wrap. Sworn by some of the greatest athletes in the world, intended for lifters, cross-trainers and go-getters, and still the gold standard that delivers day after day.",
                    Price = 160,
                }
                );
            /*    modelBuilder.Entity<Size>().HasData(
                    new Size { SizeId = 1, SizeValue = 36 },
                    new Size { SizeId = 2, SizeValue = 37 },
                    new Size { SizeId = 3, SizeValue = 38 },
                    new Size { SizeId = 4, SizeValue = 39 },
                    new Size { SizeId = 5, SizeValue = 40 },
                    new Size { SizeId = 6, SizeValue = 41 },
                    new Size { SizeId = 7, SizeValue = 42 },
                    new Size { SizeId = 8, SizeValue = 43 },
                    new Size { SizeId = 9, SizeValue = 44 },
                    new Size { SizeId = 10, SizeValue = 45 },
                    new Size { SizeId = 11, SizeValue = 46 },
                    new Size { SizeId = 12, SizeValue = 47 }
                    );
                modelBuilder.Entity<Gender>().HasData(
                    new Gender { GenderId = 1, GenderName = "Men" },
                    new Gender { GenderId = 2, GenderName = "Women" },
                    new Gender { GenderId = 3, GenderName = "Kids" },
                    new Gender { GenderId = 4, GenderName = "Unisex" }
                    );*/
        }
    }
}
