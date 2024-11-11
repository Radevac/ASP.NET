using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toyota_Cars.Data;
using WebApplicationGeo.Models.ViewModels;
using WebApplicationGeo.Models.Cars.Toyota;

namespace WebApplicationGeo.Controllers.Toyota
{
    public class ColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Colors
        public async Task<IActionResult> Index(
            int pageNumber = 1, int pageSize = 2,
            string sortColumn = "Name", string sortDirection = "asc"
            )
        {
            var query = _context.Colors.AsQueryable();

            // Сортировка
            query = sortColumn switch
            {
                // Якщо сортировка по Id
                "Id" => sortDirection == "asc" ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id),
                // Якщо сортировка по Name
                "Name" => sortDirection == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
                // За замовченням
                _ => sortDirection == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
            };
            

            var totalItems = await query.CountAsync();

            var colors = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            // // также необходимо обновить SelectList для столбцов сортировки
            // var columns = new List<SelectListItem>
            // {
            //     new SelectListItem { Value = "Name", Text = "Name" },
            //     new SelectListItem { Value = "Id", Text = "Id" },
            //     // Добавьте другие возможные столбцы
            // };
    
            // Используем рефлексию для получения свойств модели ColorModel
            var properties = typeof(ColorModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Создаем список SelectListItem на основе свойств модели
            var columns = properties.Select(prop => new SelectListItem 
            { 
                Value = prop.Name, 
                Text = prop.Name 
            }).ToList();

            SelectList sort = new SelectList(columns, "Value", "Text", sortColumn);
            
            // Определяем возможные значения для направления сортировки
            var sortDirections = new List<SelectListItem>
            {
                new SelectListItem { Value = "asc", Text = "Ascending" },
                new SelectListItem { Value = "desc", Text = "Descending" }
            };
             SelectList dir = new SelectList(sortDirections, "Value", "Text", sortDirection);
            

            ViewData["Paginate"] = new PaginateViewModel
            {
                // Для пагинации
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                
                // Для сортировки
                SortColumn = sortColumn,
                SortColumnSelectedList = sort,
                
                SortDirection = sortDirection,
                SortDirectionSelectedList = dir,
                
                Columns = new List<string>(["Id","Name"])
            };

            return View(colors);
        }
        
        // GET: Colors
        public async Task<IActionResult> IndexPaginate(
            int pageNumber = 1, int pageSize = 2
        )
        {
            var totalItems = await _context.Colors.CountAsync();
            
            
            var colors = await _context.Colors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            ViewData["Paginate"] = new PaginateViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            
            return View(colors);
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorModel == null)
            {
                return NotFound();
            }

            return View(colorModel);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,RGB,Code")] ColorModel colorModel,
            IFormFile file
            )
        {
            
            string baseUrl = "/storage/colors";
        
            // Проверяем, загружен ли файл
            if (file != null && file.Length > 0)
            {
                // Указываем путь для сохранения файла
                var filePath = Path.Combine(Directory.GetCurrentDirectory()
                    , "wwwroot" + baseUrl, file.FileName);

                // Сохраняем файл на сервере
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            
                colorModel.Url = baseUrl + "/" + file.FileName;
            }
            
            
                _context.Add(colorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Colors.FindAsync(id);
            if (colorModel == null)
            {
                return NotFound();
            }
            return View(colorModel);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url,RGB,Code")] ColorModel colorModel)
        {
            if (id != colorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorModelExists(colorModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(colorModel);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorModel == null)
            {
                return NotFound();
            }

            return View(colorModel);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colorModel = await _context.Colors.FindAsync(id);
            if (colorModel != null)
            {
                _context.Colors.Remove(colorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorModelExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
