using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class PageController : Controller
{
    public IActionResult About()
    {
        return View();
    }
}