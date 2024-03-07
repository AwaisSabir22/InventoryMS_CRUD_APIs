
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Reflection;

namespace InventoryMS_CRUD_APIs.Models
{
    public class ApplicationDBContext: DbContext
    {
        /* 
         * constructor initializes the ApplicationDBContext class by passing
         *  configuration options to the base class (DbContext),
         * allowing it to establish a connection to the database and set up the context for interacting with it.
        */
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

        /* 
         * DbSet properties are used to query and save instances of the Product and Category classes.
         */
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        
        /*
         * The OnModelCreating method is used to configure the relationships between the entities.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //conifguring the product entity in the model
            modelBuilder.Entity<Product>()
            //one to many relationship between category and product
                .HasOne(p => p.Category)
            //specifies the inverse navigation property
                .WithMany(c => c.Products)
            //specifies the foreign key property
                .HasForeignKey(p => p.CategoryId)
            //specifies the delete behavior, when a category entity deleted all associated products also deleted.
                .OnDelete(DeleteBehavior.Cascade);

            // Configure soft delete behavior for the Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false); // Default value for IsDeleted

            // Ignore soft deleted records by default
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => !p.IsDeleted);


            //conifguring the category entity in the model with dummy data
            modelBuilder.Entity<Category>().HasData(
                              new Category { CategoryId = 1, CategoryName = "Electronics" },
                                             new Category { CategoryId = 2, CategoryName = "Clothes" },
                                                            new Category { CategoryId = 3, CategoryName = "Grocery" }
                                                                       );

            //conifguring the product entity in the model with dummy data
            modelBuilder.Entity<Product>().HasData(
                               new Product { ProductId = 1, ProductName = "Laptop", ProductDescription = "Dell Inspiron 15 3000", CategoryId = 1 },
                                              new Product { ProductId = 2, ProductName = "T-shirt", ProductDescription = "Polo T-shirt", CategoryId = 2 },
                                                             new Product { ProductId = 3, ProductName = "Rice", ProductDescription = "Basmati Rice", CategoryId = 3 }
                                                                        );


        }


       
        

    }
}
