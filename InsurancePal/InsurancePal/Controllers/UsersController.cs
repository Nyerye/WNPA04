/// <file>
/// UserController.cs
/// </file>
/// <project>
/// Windows Network Programming Assignment 4
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// April 9 2026
/// </date>
/// <description>
/// Controller for user account management.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using InsurancePal.Models;
using InsurancePal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// UserController class. Handles administrative user management operations,
/// including creating, editing, and deleting users. Different from AccountController
/// which handles login and cookie authentication.
/// </summary>
[Authorize(Policy = "AdminOnly")]
public class UsersController : Controller
{
    //Database context for accessing user records
    private readonly UserContext _context;

    //Password hasher used to securely hash user passwords
    private readonly PasswordHasher<User> _passwordHasher = new();

    /// <summary>
    /// Constructor that injects the UserContext dependency.
    /// </summary>
    /// <param name="context">Database context for user data.</param>
    public UsersController(UserContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays a list of all registered users.
    /// Only accessible to administrators.
    /// </summary>
    /// <returns>User list view.</returns>
    public IActionResult Index()
    {
        //Retrieve all users from the database
        var users = _context.Users.ToList();
        return View(users);
    }

    /// <summary>
    /// Displays the user creation form.
    /// </summary>
    /// <returns>Create view.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Processes user creation requests.
    /// </summary>
    /// <param name="model">User creation form data.</param>
    /// <returns>Redirects to Home on success, or redisplays form on failure.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        //Ensure the form is valid
        if (!ModelState.IsValid)
            return View(model);

        //Check if the username already exists
        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
        {
            ModelState.AddModelError("", "Username already exists.");
            return View(model);
        }

        //Create the new user
        var user = new User { Username = model.Username };

        //Hash the password before storing it
        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        //Save the new user
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Displays the edit form for a specific user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Edit view.</returns>
    public IActionResult Edit(int id)
    {
        //Find the user
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    /// <summary>
    /// Processes password updates for a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="password">New password.</param>
    /// <returns>Redirects to Index.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, string password)
    {
        //Find the user
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        //Hash and update the password
        var hasher = new PasswordHasher<User>();
        user.PasswordHash = hasher.HashPassword(user, password);

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the delete confirmation page for a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Delete view.</returns>
    public IActionResult Delete(int id)
    {
        //Find the user
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    /// <summary>
    /// Confirms deletion of a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Redirects to Index.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        //Find the user
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();

        //Remove the user
        _context.Users.Remove(user);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
