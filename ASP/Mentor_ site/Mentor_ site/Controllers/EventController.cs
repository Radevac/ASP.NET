using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class EventController : Controller
{
    public IActionResult Occupation()
    {
        return View();
    }
}