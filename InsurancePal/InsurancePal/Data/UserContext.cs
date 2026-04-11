/// <file>
/// UserContext.cs
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
/// Database context class responsible for managing User entities within
/// the InsurancePal application. Inherits from Entity Framework's DbContext
/// to enable querying and persisting User records.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using Microsoft.EntityFrameworkCore;
using InsurancePal.Models;

namespace InsurancePal.Data
{
    /// <summary>
    /// Database context for interacting with User records.
    /// Inherits from Entity Framework's DbContext.
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Constructor that accepts DbContextOptions and passes them
        /// to the base DbContext constructor.
        /// </summary>
        /// <param name="options">Configuration options for the context.</param>
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet representing the Users table in the database.
        /// Allows querying and saving User entities.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
