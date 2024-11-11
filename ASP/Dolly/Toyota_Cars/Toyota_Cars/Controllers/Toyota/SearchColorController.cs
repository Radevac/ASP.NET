using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toyota_Cars.Data;

namespace WebApplicationGeo.Controllers.Toyota;

public class SearchColorController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchColorController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET
    
    // 1 крок - Controller from url (path)
    public IActionResult Index(
        string search = null,
    int pageNumber = 1, int pageSize = 2
        )
    {
        if (pageNumber < 0) pageNumber = 0;
        if (pageSize < 0 || pageSize > 20) pageSize = 5;
        
        ViewBag.Search = search;
        
        var colors = _context.Colors
            .Include(c => c.Configurations)
            .Where(c => !String.IsNullOrEmpty(search) 
                ? EF.Functions.Like(c.Name, "%" + search + "%") : true)
            .OrderBy(c => c.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return View(colors.ToList());
        
        //
        // // 2 крок (Inlcude)
        // var colors = _context.Colors;
        // // colors.Include(colors => colors.Configurations);
        //
        // // 3 крок (Where - Filter )
        // if (!String.IsNullOrEmpty(search))
        // {
        //
        //     colors.Where(
        //         c => EF.Functions.Like(c.Name, "%" + search + "%")
        //     );
        //
        //     // colors.Where(
        //     //     color => color.Name.ToLower().Contains(search.ToLower()));
        // }
        //
        // // 4 - Paginate
        // if (pageNumber < 0) pageNumber = 0;
        // if (pageSize < 0 || pageSize > 20) pageSize = 5;
        //
        // colors.Skip((pageNumber - 1) * pageSize);
        // colors.Take(pageSize);
        //
        // // 5 - Order By
        // colors.OrderBy(c=> c.Name);
        //
        // return View(colors.ToList());
    }
}