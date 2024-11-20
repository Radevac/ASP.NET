using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toyota_Pagination.Data;
using WebApplicationGeo.Models.Cars.Toyota;

namespace WebApplicationGeo.Controllers.Toyota
{
    public class CarModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToyotaModels.ToListAsync());
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // GET: CarModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ToyotaModel toyotaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toyotaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toyotaModel);
        }

        // GET: CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels.FindAsync(id);
            if (toyotaModel == null)
            {
                return NotFound();
            }
            return View(toyotaModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ToyotaModel toyotaModel)
        {
            if (id != toyotaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toyotaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyotaModelExists(toyotaModel.Id))
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
            return View(toyotaModel);
        }

        // GET: CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.ToyotaModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toyotaModel = await _context.ToyotaModels.FindAsync(id);
            if (toyotaModel != null)
            {
                _context.ToyotaModels.Remove(toyotaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyotaModelExists(int id)
        {
            return _context.ToyotaModels.Any(e => e.Id == id);
        }
    }
}
