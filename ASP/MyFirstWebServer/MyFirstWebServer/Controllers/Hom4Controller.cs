using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class Homework4Controller : Controller
{
    public IActionResult Hom4()
    {
        return View();
    }
}