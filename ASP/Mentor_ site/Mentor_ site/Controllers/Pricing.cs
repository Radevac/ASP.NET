using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class Pricing : Controller
{
    public IActionResult Price()
    {
        return View();
    }
}