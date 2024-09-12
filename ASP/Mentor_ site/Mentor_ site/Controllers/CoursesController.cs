using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class CoursesController : Controller
{
    public IActionResult Prise()
    {
        return View();
    }
}