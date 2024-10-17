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
            return View(await _context.AreaModel.ToListAsync());
        }

        // GET: Area/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel
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
            return View();
        }

        // POST: Area/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AreaModel area)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area); 
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(area);
        }

        // GET: Area/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel.FindAsync(id);
            if (areaModel == null)
            {
                return NotFound();
            }
            return View(areaModel);
        }

        // POST: Area/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug")] AreaModel areaModel)
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
            return View(areaModel);
        }

        // GET: Area/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaModel = await _context.AreaModel
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
            var areaModel = await _context.AreaModel.FindAsync(id);
            if (areaModel != null)
            {
                _context.AreaModel.Remove(areaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaModelExists(int id)
        {
            return _context.AreaModel.Any(e => e.Id == id);
        }
    }
}
