using Microsoft.EntityFrameworkCore;
 
namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }

        public DbSet<Person> Users {get; set;}
        public DbSet<Wedding> Weddings {get; set;}
        public DbSet<UserWedd> UserWedding {get; set;}

    }
}