/// <file>
/// ItemContext.cs
/// </file>
/// <project>
/// Windows Network Programming Assignment 4
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// April 10 2026
/// </date>
/// <description>
/// Database context class responsible for managing Item entities within
/// the InsurancePal application. Inherits from Entity Framework's DbContext
/// to enable querying and persisting Item records.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using InsurancePal.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Data
{
    /// <summary>
    /// Database context for interacting with Item records.
    /// Inherits from Entity Framework's DbContext.
    /// </summary>
    public class ItemContext : DbContext
    {
        /// <summary>
        /// Constructor that accepts DbContextOptions and passes them
        /// to the base DbContext constructor.
        /// </summary>
        /// <param name="options">Configuration options for the context.</param>
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet representing the Items table in the database.
        /// Allows querying and saving Item entities.
        /// </summary>
        public DbSet<Item> Items { get; set; }
    }
}
