using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toyota_Cars.Data;

namespace WebApplicationGeo.Controllers.Toyota
{
    public class ShowAllCarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowAllCarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Colors()
        {
            var colors = _context.Colors
                    .Include(color=> color.Configurations)
                    .ThenInclude(config => config.Configuration)
                    .ThenInclude(config=> config.Model)
                    .ToList();
            
            return View(colors);
        }
        
        
        // GET: ShowAllCarsController
        public ActionResult Index()
        {
            var allModels 
                 =  _context.ToyotaModels
                .Include(tModels => tModels.Configurations)
                    .ThenInclude(tConfig => tConfig.Colors )
                        .ThenInclude(tColors => tColors.Color)
                        
                .ToList();
            
            return View(allModels);
        }

    }
}
