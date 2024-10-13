using Microsoft.AspNetCore.Mvc;
using MyFirstWebServer.Data;
using MyFirstWebServer.Models.Entities;

namespace MyFirstWebServer.Controllers;

public class SubscribeController : Controller
{

    private readonly ApplicationDbContext _context;

    public SubscribeController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///  Add user email to DB
    /// </summary>
    /// <param name="email">User Email</param>
    /// <returns></returns>
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveEmail(String email)
    {
        ViewBag.email = email;

        SubscribeModel newSubscriber = new SubscribeModel();
        newSubscriber.Email = email;

        //ApplicationDbContext db = new ApplicationDbContext();

        _context.Subscribers.Add(newSubscriber);
        _context.SaveChangesAsync();
        
        return View();
    }
    
}