using InsurancePal.Models;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        if (!User.HasClaim("IsAdmin", "true"))
            return Forbid();

        return View();
    }
}
