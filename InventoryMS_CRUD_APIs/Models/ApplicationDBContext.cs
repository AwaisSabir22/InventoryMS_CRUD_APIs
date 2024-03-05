
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Reflection;

namespace InventoryMS_CRUD_APIs.Models
{
    public class ApplicationDBContext: DbContext
    {
        //constructor initializes the ApplicationDBContext class by passing
        //configuration options to the base class (DbContext),
        //allowing it to establish a connection to the database and set up the context for interacting with it.
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        //DbSet properties are used to query and save instances of the Product and Category classes.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
