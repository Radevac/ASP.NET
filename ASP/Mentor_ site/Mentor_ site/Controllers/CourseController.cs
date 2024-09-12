using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class CourseController : Controller
{
    public IActionResult Direction()
    {
        return View();
    }
}