/// <file>
/// HomeController.cs
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
/// Controller responsible for generating the landing page dashboard
/// and displaying summary statistics for the logged-in user.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Controllers
{
    /// <summary>
    /// Handles routing and logic for the home landing page.
    /// Displays dashboard statistics for the logged-in user.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        //Database context used to retrieve item data
        private readonly ItemContext _context;

        /// <summary>
        /// Constructor that injects the ItemContext dependency.
        /// </summary>
        /// <param name="context">Database context for item data.</param>
        public HomeController(ItemContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Landing page action that generates dashboard statistics
        /// for the currently logged-in user.
        /// </summary>
        /// <returns>The dashboard view populated with summary statistics.</returns>
        public async Task<IActionResult> Index()
        {
            //Retrieve the username of the logged-in user
            var username = User.Identity?.Name;

            //Pull all items belonging to the logged-in user
            var items = await _context.Items
                .Where(i => i.OwnerID == username)
                .ToListAsync();

            //Build the dashboard model
            var model = new HomeDashboardViewModel
            {
                TotalItems = items.Count,
                TotalValue = items.Sum(i => i.EstimatedValue),
                UniqueRooms = items.Select(i => i.Room).Distinct().Count(),
                UniqueCategories = items.Select(i => i.Category).Distinct().Count()
            };

            //Return the populated dashboard to the view
            return View(model);
        }
    }
}
