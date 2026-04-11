/// <file>
/// AdminController.cs
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
/// Controller responsible for administrative user management operations.
/// Only accessible to users with administrative privileges.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    //Database context for accessing user records
    private readonly UserContext _context;

    /// <summary>
    /// Constructor that injects the UserContext dependency.
    /// </summary>
    /// <param name="context">Database context for user data.</param>
    public AdminController(UserContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Displays a list of all registered users.
    /// Only accessible to administrators.
    /// </summary>
    /// <returns>The user list view or a 403 Forbidden result.</returns>
    public async Task<IActionResult> Index()
    {
        //Ensure the current user is an administrator
        if (!User.HasClaim("IsAdmin", "true"))
            return Forbid();

        //Retrieve all users from the database
        var users = await _context.Users.ToListAsync();

        //Return the user list to the view
        return View(users);
    }
}
