using Microsoft.AspNetCore.Mvc;
using Toyota_Cars.Data;
using WebApplicationGeo.Models.Cars.Toyota;


public class ColorsHandMadeController : Controller
{


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name", "RGB", "Code")] ColorModel colorModel,
        IFormFile file
        )
    {
        string baseUrl = "/storage/colors";
        
        // Проверяем, загружен ли файл
        if (file != null && file.Length > 0)
        {
            // Указываем путь для сохранения файла
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + baseUrl, file.FileName);

            // Сохраняем файл на сервере
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            colorModel.Url = baseUrl + "/" + file.FileName;
        }
        
        
            _context.Add(colorModel);
            await _context.SaveChangesAsync();

        
        return RedirectToAction(nameof(Create));
    }
    
    
    private readonly ApplicationDbContext _context;
    
    public ColorsHandMadeController (ApplicationDbContext context)
    {
        _context = context;
    }
}