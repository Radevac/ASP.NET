using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebServer.Controllers;

public class WildAnimals : Controller
{
    public string wolf()
    {
        return "I am not a friend of man";
    }

    public string fox()
    {
        return "i'm red";
    }
    
}