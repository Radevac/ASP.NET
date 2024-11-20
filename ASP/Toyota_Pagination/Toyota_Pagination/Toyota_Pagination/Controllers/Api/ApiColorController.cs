using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toyota_Pagination.Data;
using WebApplicationGeo.Models.Cars.Toyota;
using WebApplicationGeo.Models.ViewModels;

namespace WebApplicationGeo.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiColorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiColorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiColor
        [HttpGet]
        public async Task<ActionResult<ApiPaginateResponse<ColorModel>>> 
            GetColors(
                int pageNumber = 1, int pageSize = 20,
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

            
            // Используем рефлексию для получения свойств модели ColorModel
            var properties = typeof(ColorModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
            

            var paginate = new PaginateViewModel
            {
                // Для пагинации
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                
                // Для сортировки
                SortColumn = sortColumn,
                SortColumnSelectedList = null,
                
                SortDirection = sortDirection,
                SortDirectionSelectedList = null,
                
                Columns = new List<string>(["Id","Name"])
            };
            
            var response = new ApiPaginateResponse<ColorModel>()
                                       {
                                           Data = colors,
                                           Paginate = paginate
                                       };
            
            return Ok(response);
        }

        // GET: api/ApiColor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorModel>> GetColorModel(int id)
        {
            var colorModel = await _context.Colors.FindAsync(id);

            if (colorModel == null)
            {
                return NotFound();
            }

            return Ok(colorModel);
        }

        // PUT: api/ApiColor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColorModel(int id, ColorModel colorModel)
        {
            if (id != colorModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(colorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        
        
        
        
        // POST: api/ApiColor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColorModel>> PostColorModel(
            [FromForm] ColorModel colorModel, 
            [FromForm] IFormFile file
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
            
            _context.Colors.Add(colorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColorModel",
                new { id = colorModel.Id }, colorModel);
        }
        
        
        

        // DELETE: api/ApiColor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColorModel(int id)
        {
            var colorModel = await _context.Colors.FindAsync(id);
            if (colorModel == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(colorModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorModelExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
