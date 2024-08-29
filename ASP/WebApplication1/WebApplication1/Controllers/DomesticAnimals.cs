using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class DomesticAnimals : Controller
{
    public string parrot()
    {
        return "My name is Kisha";
    }

    public string Dog()
    {
        return "I am a friend of man";
    }
}