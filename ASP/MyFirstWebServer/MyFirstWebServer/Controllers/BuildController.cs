using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class BuildController : Controller
{

    public IActionResult First()
    {
        string H1 = "Hello Vasil";
        ViewData["h1"] = H1;
        ViewBag.h1 = H1;
        
        return View();
    }
    
}