using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class PageController : Controller
{
    public IActionResult About()
    {
        return View();
    }
}