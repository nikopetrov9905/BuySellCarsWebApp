using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuySellCarsWebApp.Data;
using BuySellCarsWebApp.Models;

namespace BuySellCarsWebApp.Controllers
{
    public class CarPartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarPartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarPart
        public async Task<IActionResult> Index()
        {
              return _context.CarParts != null ? 
                          View(await _context.CarParts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CarParts'  is null.");
        }

        // GET: CarPart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // GET: CarPart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] CarPart carPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carPart);
        }

        // GET: CarPart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts.FindAsync(id);
            if (carPart == null)
            {
                return NotFound();
            }
            return View(carPart);
        }

        // POST: CarPart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] CarPart carPart)
        {
            if (id != carPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartExists(carPart.Id))
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
            return View(carPart);
        }

        // GET: CarPart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // POST: CarPart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarParts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarParts'  is null.");
            }
            var carPart = await _context.CarParts.FindAsync(id);
            if (carPart != null)
            {
                _context.CarParts.Remove(carPart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPartExists(int id)
        {
          return (_context.CarParts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
