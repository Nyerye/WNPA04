using InsurancePal.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Data
{
    /// <summary>
    /// ApplicationDbContext class that inherits from the DbContext class
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        //Constructor for the ApplicationDbContext class that takes in DbContextOptions and passes it to the base class constructor.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Method that sets the precison for the Microsoft Entity Framework to apply to the EstimatedValue property for Item class.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(i => i.EstimatedValue)
                .HasPrecision(10, 2);

            base.OnModelCreating(modelBuilder);
        }

        //DbSet property for the Item model, which allows for querying and saving instances of the Item class.
        //Allows for the database to make an Items table.
        public DbSet<Item> Items { get; set; }
    }
}
