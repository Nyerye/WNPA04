using InsurancePal.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Data
{
    /// <summary>
    /// ApplicationDbContext class that inherits from the DbContext abstract class from Entity Frame Work.
   ///</summary>
    public class ItemContext : DbContext
    {
        //Constructor for the ItemContext class that takes in DbContextOptions and passes it to the base class constructor.
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {

        }

        //DbSet property for the Item model, which allows for querying and saving instances of the Item class.
        //Allows for the database to make an Items table.
        public DbSet<Item> Items { get; set; }
    }
}
