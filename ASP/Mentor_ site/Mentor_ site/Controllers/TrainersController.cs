using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class TrainersController : Controller
{
    public IActionResult People()
    {
        return View();
    }
}