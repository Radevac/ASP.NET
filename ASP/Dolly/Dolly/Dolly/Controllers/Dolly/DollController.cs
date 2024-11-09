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
    public class DollController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DollController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doll
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dolls.ToListAsync());
        }

        // GET: Doll/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dollModel = await _context.Dolls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dollModel == null)
            {
                return NotFound();
            }

            return View(dollModel);
        }

        // GET: Doll/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doll/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DollModel dollModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dollModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dollModel);
        }

        // GET: Doll/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dollModel = await _context.Dolls.FindAsync(id);
            if (dollModel == null)
            {
                return NotFound();
            }
            return View(dollModel);
        }

        // POST: Doll/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DollModel dollModel)
        {
            if (id != dollModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dollModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DollModelExists(dollModel.Id))
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
            return View(dollModel);
        }

        // GET: Doll/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dollModel = await _context.Dolls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dollModel == null)
            {
                return NotFound();
            }

            return View(dollModel);
        }

        // POST: Doll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dollModel = await _context.Dolls.FindAsync(id);
            if (dollModel != null)
            {
                _context.Dolls.Remove(dollModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DollModelExists(int id)
        {
            return _context.Dolls.Any(e => e.Id == id);
        }
    }
}
