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
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: House
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HouseModel.Include(h => h.Street);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: House/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houseModel = await _context.HouseModel
                .Include(h => h.Street)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (houseModel == null)
            {
                return NotFound();
            }

            return View(houseModel);
        }

        // GET: House/Create
        public IActionResult Create()
        {
            ViewData["StreetId"] = new SelectList(_context.Set<StreetModel>(), "Id", "Id");
            return View();
        }

        // POST: House/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Slug,StreetId")] HouseModel houseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(houseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StreetId"] = new SelectList(_context.Set<StreetModel>(), "Id", "Id", houseModel.StreetId);
            return View(houseModel);
        }

        // GET: House/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houseModel = await _context.HouseModel.FindAsync(id);
            if (houseModel == null)
            {
                return NotFound();
            }
            ViewData["StreetId"] = new SelectList(_context.Set<StreetModel>(), "Id", "Id", houseModel.StreetId);
            return View(houseModel);
        }

        // POST: House/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Slug,StreetId")] HouseModel houseModel)
        {
            if (id != houseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(houseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseModelExists(houseModel.Id))
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
            ViewData["StreetId"] = new SelectList(_context.Set<StreetModel>(), "Id", "Id", houseModel.StreetId);
            return View(houseModel);
        }

        // GET: House/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var houseModel = await _context.HouseModel
                .Include(h => h.Street)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (houseModel == null)
            {
                return NotFound();
            }

            return View(houseModel);
        }

        // POST: House/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var houseModel = await _context.HouseModel.FindAsync(id);
            if (houseModel != null)
            {
                _context.HouseModel.Remove(houseModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseModelExists(int id)
        {
            return _context.HouseModel.Any(e => e.Id == id);
        }
    }
}
