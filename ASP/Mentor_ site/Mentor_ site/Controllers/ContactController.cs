using Microsoft.AspNetCore.Mvc;

namespace Mentor__site.Controllers;

public class ContactController : Controller
{
    public IActionResult Form()
    {
        return View();
    }
}