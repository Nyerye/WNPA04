using InsurancePal.Data;
using InsurancePal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private readonly UserContext _context;
    public AdminController(UserContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        if (!User.HasClaim("IsAdmin", "true"))
            return Forbid();

        var users = await _context.Users.ToListAsync();
        return View(users);
    }

}
