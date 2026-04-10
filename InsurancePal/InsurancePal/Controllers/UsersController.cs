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
/// 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using InsurancePal.Models;
using InsurancePal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


/// <summary>
/// UserController class. Has methods for creating a new user. Different from AccountController as it processes logins and cookie creation.
/// </summary>
/// 

[Authorize(Policy = "AdminOnly")]
public class UsersController : Controller
{
    private readonly UserContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UsersController(UserContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
        {
            ModelState.AddModelError("", "Username already exists.");
            return View(model);
        }

        var user = new User { Username = model.Username };
        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}
