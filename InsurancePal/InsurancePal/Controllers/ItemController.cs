using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Authorization;

namespace InsurancePal.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
        }
        private void PopulateDropdowns()
        {
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Furniture",
                "Appliances",
                "Jewelry",
                "Clothing",
                "Sports Equipment",
                "Tools",
                "Other"
            };

            ViewBag.Rooms = new List<string>
            {
                "Living Room",
                "Kitchen",
                "Master Bedroom",
                "Bedroom 2",
                "Bedroom 3",
                "Bedroom 4",
                "Bathroom",
                "Basement",
                "Garage",
                "Office",
                "Dining Room",
                "Storage"
            };
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            try
            {
                var username = User.Identity?.Name;

                var items = await _context.Items
                    .Where(i => i.OwnerID == username)
                    .ToListAsync();

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Category,Room,EstimatedValue,purchasedDate,Description")] Item item)
        {
            PopulateDropdowns();

            item.OwnerID = User.Identity.Name;
            ModelState.Remove("OwnerID");

            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            PopulateDropdowns();
            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item updatedItem)
        {
            //Populate the Category and Room drop down menus.
            PopulateDropdowns();

            //Confirm the ID and the updateItems ID are both the same.
            if (id != updatedItem.ItemId)
                return NotFound();

            //Confirm that the item actually exists in the database.
            var existingItem = await _context.Items.FindAsync(id);
            if (existingItem == null)
                return NotFound();

            //Update fields manually
            existingItem.Name = updatedItem.Name;
            existingItem.Category = updatedItem.Category;
            existingItem.Room = updatedItem.Room;
            existingItem.EstimatedValue = updatedItem.EstimatedValue;
            existingItem.purchasedDate = updatedItem.purchasedDate;
            existingItem.Description = updatedItem.Description;

            //Preserve ownership of the item
            existingItem.OwnerID = User.Identity.Name;

            //Save the results back to the database
            await _context.SaveChangesAsync();

            //Send back to the Index page.
            return RedirectToAction(nameof(Index));
        }



        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null)
                _context.Items.Remove(item);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
