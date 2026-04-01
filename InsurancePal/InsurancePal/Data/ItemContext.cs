using InsurancePal.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Data
{
    /// <summary>
    /// ApplicationDbContext class that inherits from the DbContext abstract class from Entity Frame Work.
    /// 
    /// NORBERT: I did research on the demo. While the demo does not employ the same thing, it seeds the data manually afterwards in the page.
    ///          To my knowledge as of March 31 2026, you will expect for me to be able to add things into it. I can not manually seed.
    ///          In order to facilitate, this class must inherit the DbContext class so my Models are aware of whats going on.
    ///          The tutorial even refernced the MvcMovieContext class, which is the same thing as this class, but for that project.
    /// </summary>
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
