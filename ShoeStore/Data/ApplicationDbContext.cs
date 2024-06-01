using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;

namespace ShoeStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        DbSet<Category> Categories { get; set; }
        DbSet<Shoe> Shoes { get; set; }
    }
}
