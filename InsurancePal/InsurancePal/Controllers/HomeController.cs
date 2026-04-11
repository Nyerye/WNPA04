using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurancePal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ItemContext _context;

        public HomeController(ItemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
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

            return View(model);
        }
    }
}
