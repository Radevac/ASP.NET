using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationGeo_2.Data;
using WebApplicationGeo_2.Models.Entities.Geo;

namespace WebApplicationGeo_2.Controllers.Geo
{
    public class AreaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Area
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Areas.Include(a => a.Country).Include(a => a.RegionCapital);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Area/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.Areas
                .Include(a => a.Country)
                .Include(a => a.RegionCapital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaModel == null)
            {
                return NotFound();
            }

            return View(areaModel);
        }

        // GET: Area/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["RegionCapitalId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Area/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegionCapitalId,CountryId")] AreaModel areaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", areaModel.CountryId);
            ViewData["RegionCapitalId"] = new SelectList(_context.Cities, "Id", "Id", areaModel.RegionCapitalId);
            return View(areaModel);
        }

        // GET: Area/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.Areas.FindAsync(id);
            if (areaModel == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", areaModel.CountryId);
            ViewData["RegionCapitalId"] = new SelectList(_context.Cities, "Id", "Id", areaModel.RegionCapitalId);
            return View(areaModel);
        }

        // POST: Area/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegionCapitalId,CountryId")] AreaModel areaModel)
        {
            if (id != areaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaModelExists(areaModel.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", areaModel.CountryId);
            ViewData["RegionCapitalId"] = new SelectList(_context.Cities, "Id", "Id", areaModel.RegionCapitalId);
            return View(areaModel);
        }

        // GET: Area/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.Areas
                .Include(a => a.Country)
                .Include(a => a.RegionCapital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaModel == null)
            {
                return NotFound();
            }

            return View(areaModel);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var areaModel = await _context.Areas.FindAsync(id);
            if (areaModel != null)
            {
                _context.Areas.Remove(areaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaModelExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
