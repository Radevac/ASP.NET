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
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Country
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Countries.Include(c => c.Capital);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Country/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryModel = await _context.Countries
                .Include(c => c.Capital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryModel == null)
            {
                return NotFound();
            }

            return View(countryModel);
        }

        // GET: Country/Create
        public IActionResult Create()
        {
            ViewData["CapitalId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Country/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CapitalId")] CountryModel countryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CapitalId"] = new SelectList(_context.Cities, "Id", "Name", countryModel.CapitalId);
            return View(countryModel);
        }

        // GET: Country/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryModel = await _context.Countries.FindAsync(id);
            if (countryModel == null)
            {
                return NotFound();
            }
            ViewData["CapitalId"] = new SelectList(_context.Cities, "Id", "Id", countryModel.CapitalId);
            return View(countryModel);
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CapitalId")] CountryModel countryModel)
        {
            if (id != countryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryModelExists(countryModel.Id))
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
            ViewData["CapitalId"] = new SelectList(_context.Cities, "Id", "Id", countryModel.CapitalId);
            return View(countryModel);
        }

        // GET: Country/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryModel = await _context.Countries
                .Include(c => c.Capital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryModel == null)
            {
                return NotFound();
            }

            return View(countryModel);
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countryModel = await _context.Countries.FindAsync(id);
            if (countryModel != null)
            {
                _context.Countries.Remove(countryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryModelExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
