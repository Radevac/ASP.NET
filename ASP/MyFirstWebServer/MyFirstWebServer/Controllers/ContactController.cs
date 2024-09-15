using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class ContactController : Controller
{
    public IActionResult Form()
        {
            return View();
        }
}