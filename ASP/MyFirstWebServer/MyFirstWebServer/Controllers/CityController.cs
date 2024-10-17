using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstWebServer.Data;
using MyFirstWebServer.Models.Entities;

namespace MyFirstWebServer.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: City
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CityModel.Include(c => c.Area);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.CityModel
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityModel == null)
            {
                return NotFound();
            }

            return View(cityModel);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.AreaModel, "Id", "Id");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,AreaId")] CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.AreaModel, "Id", "Id", cityModel.AreaId);
            return View(cityModel);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.CityModel.FindAsync(id);
            if (cityModel == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.AreaModel, "Id", "Id", cityModel.AreaId);
            return View(cityModel);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,AreaId")] CityModel cityModel)
        {
            if (id != cityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityModelExists(cityModel.Id))
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
            ViewData["AreaId"] = new SelectList(_context.AreaModel, "Id", "Id", cityModel.AreaId);
            return View(cityModel);
        }

        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.CityModel
                .Include(c => c.Area)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityModel == null)
            {
                return NotFound();
            }

            return View(cityModel);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityModel = await _context.CityModel.FindAsync(id);
            if (cityModel != null)
            {
                _context.CityModel.Remove(cityModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityModelExists(int id)
        {
            return _context.CityModel.Any(e => e.Id == id);
        }
    }
}
