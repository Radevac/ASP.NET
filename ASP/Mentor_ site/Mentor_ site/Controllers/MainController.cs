using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class MainController : Controller
{
    public IActionResult Home()
    {
        return View();
    }
}