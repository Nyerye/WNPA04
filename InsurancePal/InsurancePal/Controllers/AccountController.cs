/// <file>
/// AccountController.cs
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
/// Controller for user account management and login management.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using InsurancePal.Data;
using InsurancePal.Models;
using InsurancePal.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace InsurancePal.Controllers
{
    /// <summary>
    /// Handles user authentication operations such as login and logout.
    /// </summary>
    public class AccountController : Controller
    {
        //Database context for accessing user records
        private readonly UserContext _context;

        //Password hasher used to verify user credentials
        private readonly PasswordHasher<User> _passwordHasher = new();

        /// <summary>
        /// Constructor that injects the UserContext dependency.
        /// </summary>
        /// <param name="context">Database context for user data.</param>
        public AccountController(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the login page to the user.
        /// </summary>
        /// <param name="returnUrl">Optional URL to redirect to after login.</param>
        /// <returns>The login view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        /// <summary>
        /// Processes login attempts by validating user credentials.
        /// </summary>
        /// <param name="model">Login form data submitted by the user.</param>
        /// <returns>Redirects to the home page or redisplays the login form on failure.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Ensure the form is valid before processing
            if (!ModelState.IsValid)
                return View(model);

            //Attempt to locate the user by username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            //Verify the password using the stored hash
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            //Build the claims identity for authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("IsAdmin", user.IsAdmin.ToString().ToLower())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            //Authentication settings for the session
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            //Sign the user into the application
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties
            );

            //Redirect to the return URL if valid
            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return LocalRedirect(model.ReturnUrl);

            //Default redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logs the user out of the application and clears authentication cookies.
        /// </summary>
        /// <returns>Redirects to the login page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //Clear the authentication session
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
