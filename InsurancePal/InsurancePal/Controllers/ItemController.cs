/// <file>
/// ItemController.cs
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
/// Controller responsible for CRUD operations on user-owned items within
/// the InsurancePal application. Includes item creation, editing, deletion,
/// and detailed viewing. All actions require authentication.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Authorization;

namespace InsurancePal.Controllers
{
    /// <summary>
    /// Handles CRUD operations for user-owned items.
    /// </summary>
    [Authorize]
    public class ItemController : Controller
    {
        //Database context for accessing item records
        private readonly ItemContext _context;

        /// <summary>
        /// Constructor that injects the ItemContext dependency.
        /// </summary>
        /// <param name="context">Database context for item data.</param>
        public ItemController(ItemContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Populates dropdown lists for item categories and rooms.
        /// </summary>
        private void PopulateDropdowns()
        {
            //Category dropdown values
            ViewBag.Categories = new List<string>
            {
                "Electronics",
                "Furniture",
                "Appliances",
                "Jewelry",
                "Clothing",
                "Sports Equipment",
                "Tools",
                "Toys and Games",
                "Other"
            };

            //Room dropdown values
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
                "Attic",
                "Storage"
            };
        }

        /// <summary>
        /// Displays all items belonging to the logged-in user.
        /// </summary>
        /// <returns>List of items.</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                //Retrieve username of logged-in user
                var username = User.Identity?.Name;

                //Query items belonging to the user
                var items = await _context.Items
                    .Where(i => i.OwnerID == username)
                    .ToListAsync();

                return View(items);
            }
            catch (Exception ex)
            {
                //Return exception details for debugging
                return Content(ex.ToString());
            }
        }

        /// <summary>
        /// Displays detailed information about a specific item.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Item details view.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            //Retrieve the item
            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        /// <summary>
        /// Displays the item creation form.
        /// </summary>
        /// <returns>Create view.</returns>
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        /// <summary>
        /// Processes item creation requests.
        /// </summary>
        /// <param name="item">Item data submitted by the user.</param>
        /// <returns>Redirects to Index on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Category,Room,EstimatedValue,purchasedDate,Description")] Item item)
        {
            PopulateDropdowns();

            //Assign ownership to the logged-in user
            item.OwnerID = User.Identity.Name;

            //Remove OwnerID from validation since it's set manually
            ModelState.Remove("OwnerID");

            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        /// <summary>
        /// Displays the item editing form.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Edit view.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            //Retrieve the item
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            PopulateDropdowns();
            return View(item);
        }

        /// <summary>
        /// Processes item edit submissions.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <param name="updatedItem">Updated item data.</param>
        /// <returns>Redirects to Index on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item updatedItem)
        {
            //Populate dropdowns for redisplay
            PopulateDropdowns();

            //Ensure the ID matches the submitted item
            if (id != updatedItem.ItemId)
                return NotFound();

            //Retrieve the existing item
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

            //Preserve ownership
            existingItem.OwnerID = User.Identity.Name;

            //Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the delete confirmation page.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Delete view.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            //Retrieve the item
            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        /// <summary>
        /// Confirms deletion of an item.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Redirects to Index.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Find the item
            var item = await _context.Items.FindAsync(id);

            //Remove if found
            if (item != null)
                _context.Items.Remove(item);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks whether an item exists in the database.
        /// </summary>
        /// <param name="id">Item ID.</param>
        /// <returns>True if the item exists.</returns>
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
