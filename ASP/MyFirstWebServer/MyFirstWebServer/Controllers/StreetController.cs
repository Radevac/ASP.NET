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
    public class StreetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StreetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Street
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StreetModel.Include(s => s.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Street/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetModel = await _context.StreetModel
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streetModel == null)
            {
                return NotFound();
            }

            return View(streetModel);
        }

        // GET: Street/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.CityModel, "Id", "Id");
            return View();
        }

        // POST: Street/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,CityId")] StreetModel streetModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(streetModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.CityModel, "Id", "Id", streetModel.CityId);
            return View(streetModel);
        }

        // GET: Street/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetModel = await _context.StreetModel.FindAsync(id);
            if (streetModel == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.CityModel, "Id", "Id", streetModel.CityId);
            return View(streetModel);
        }

        // POST: Street/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,CityId")] StreetModel streetModel)
        {
            if (id != streetModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streetModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetModelExists(streetModel.Id))
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
            ViewData["CityId"] = new SelectList(_context.CityModel, "Id", "Id", streetModel.CityId);
            return View(streetModel);
        }

        // GET: Street/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streetModel = await _context.StreetModel
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streetModel == null)
            {
                return NotFound();
            }

            return View(streetModel);
        }

        // POST: Street/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var streetModel = await _context.StreetModel.FindAsync(id);
            if (streetModel != null)
            {
                _context.StreetModel.Remove(streetModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetModelExists(int id)
        {
            return _context.StreetModel.Any(e => e.Id == id);
        }
    }
}
