using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dolly.Data;
using Dolly.Models.Dolly;

namespace Dolly.Controllers.Dolly
{
    public class OutfitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutfitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Outfit
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Outfits.Include(o => o.Doll);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Outfit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitModel = await _context.Outfits
                .Include(o => o.Doll)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outfitModel == null)
            {
                return NotFound();
            }

            return View(outfitModel);
        }

        // GET: Outfit/Create
        public IActionResult Create()
        {
            ViewData["DollId"] = new SelectList(_context.Dolls, "Id", "Id");
            return View();
        }

        // POST: Outfit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Style,DollId")] OutfitModel outfitModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outfitModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DollId"] = new SelectList(_context.Dolls, "Id", "Id", outfitModel.DollId);
            return View(outfitModel);
        }

        // GET: Outfit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitModel = await _context.Outfits.FindAsync(id);
            if (outfitModel == null)
            {
                return NotFound();
            }
            ViewData["DollId"] = new SelectList(_context.Dolls, "Id", "Id", outfitModel.DollId);
            return View(outfitModel);
        }

        // POST: Outfit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Style,DollId")] OutfitModel outfitModel)
        {
            if (id != outfitModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outfitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutfitModelExists(outfitModel.Id))
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
            ViewData["DollId"] = new SelectList(_context.Dolls, "Id", "Id", outfitModel.DollId);
            return View(outfitModel);
        }

        // GET: Outfit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outfitModel = await _context.Outfits
                .Include(o => o.Doll)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outfitModel == null)
            {
                return NotFound();
            }

            return View(outfitModel);
        }

        // POST: Outfit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outfitModel = await _context.Outfits.FindAsync(id);
            if (outfitModel != null)
            {
                _context.Outfits.Remove(outfitModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutfitModelExists(int id)
        {
            return _context.Outfits.Any(e => e.Id == id);
        }
    }
}
