using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Toyota_Cars.Data;
using Toyota_Cars.Models.Cars.Toyota;

namespace Toyota_Cars.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortColumn = "Model", string sortDirection = "asc")
        {
            
            var cars = _context.Cars
                .Include(c => c.Color)
                .Include(c => c.Configuration)
                .AsQueryable();

            
            cars = sortColumn switch
            {
                "Color" => sortDirection == "asc" 
                    ? cars.OrderBy(c => c.Color.Name) 
                    : cars.OrderByDescending(c => c.Color.Name),
                "Model" => sortDirection == "asc" 
                    ? cars.OrderBy(c => c.Model) 
                    : cars.OrderByDescending(c => c.Model),
                _ => cars
            };

            var model = await cars.ToListAsync();
            return View(model);
        }

    }
}