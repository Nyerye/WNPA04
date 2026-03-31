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

        //DbSet property for the Item model, which allows for querying and saving instances of the Item class.
        //Allows for the database to make an Items table.
        public DbSet<Item> Items { get; set; }
    }
}
