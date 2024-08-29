
using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class FirstController : Controller
{
    public string Html()
    {
        return "<h1>Hello Html</h1>";
    }
    public string Hello()
    {
        return "Hello";
    }

    public string Bue()
    {
        return "Bue";
    }
}