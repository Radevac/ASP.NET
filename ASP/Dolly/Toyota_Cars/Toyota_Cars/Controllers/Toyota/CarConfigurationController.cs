using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationGeo.Models.Cars.Toyota;
using Toyota_Cars.Data;

namespace WebApplicationGeo.Controllers.Toyota
{
    public class CarConfigurationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarConfigurationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarConfiguration
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Configurations.Include(c => c.Model);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarConfiguration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationModel = await _context.Configurations
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configurationModel == null)
            {
                return NotFound();
            }

            return View(configurationModel);
        }

        // GET: CarConfiguration/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "Name");
            ViewBag.AllModels =  _context.ToyotaModels.ToList();
            return View();
        }

        // POST: CarConfiguration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ModelId")] ConfigurationModel configurationModel)
        {
            configurationModel.Model = _context.ToyotaModels.Find(configurationModel.ModelId);
            

                _context.Add(configurationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "Name", configurationModel.ModelId);
            return View(configurationModel);
        }

        // GET: CarConfiguration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationModel = await _context.Configurations.FindAsync(id);
            if (configurationModel == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "Name", configurationModel.ModelId);
            return View(configurationModel);
        }

        // POST: CarConfiguration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ModelId")] ConfigurationModel configurationModel)
        {
            if (id != configurationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configurationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationModelExists(configurationModel.Id))
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
            ViewData["ModelId"] = new SelectList(_context.ToyotaModels, "Id", "Name", configurationModel.ModelId);
            return View(configurationModel);
        }

        // GET: CarConfiguration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationModel = await _context.Configurations
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configurationModel == null)
            {
                return NotFound();
            }

            return View(configurationModel);
        }

        // POST: CarConfiguration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configurationModel = await _context.Configurations.FindAsync(id);
            if (configurationModel != null)
            {
                _context.Configurations.Remove(configurationModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationModelExists(int id)
        {
            return _context.Configurations.Any(e => e.Id == id);
        }
    }
}
