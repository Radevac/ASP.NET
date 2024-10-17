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
    public class CityTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CityType
        public async Task<IActionResult> Index()
        {
            return View(await _context.CityTypeModel.ToListAsync());
        }

        // GET: CityType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityTypeModel = await _context.CityTypeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityTypeModel == null)
            {
                return NotFound();
            }

            return View(cityTypeModel);
        }

        // GET: CityType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CityType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,Slug")] CityTypeModel cityTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cityTypeModel);
        }

        // GET: CityType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityTypeModel = await _context.CityTypeModel.FindAsync(id);
            if (cityTypeModel == null)
            {
                return NotFound();
            }
            return View(cityTypeModel);
        }

        // POST: CityType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,Slug")] CityTypeModel cityTypeModel)
        {
            if (id != cityTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityTypeModelExists(cityTypeModel.Id))
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
            return View(cityTypeModel);
        }

        // GET: CityType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityTypeModel = await _context.CityTypeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityTypeModel == null)
            {
                return NotFound();
            }

            return View(cityTypeModel);
        }

        // POST: CityType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityTypeModel = await _context.CityTypeModel.FindAsync(id);
            if (cityTypeModel != null)
            {
                _context.CityTypeModel.Remove(cityTypeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityTypeModelExists(int id)
        {
            return _context.CityTypeModel.Any(e => e.Id == id);
        }
    }
}
