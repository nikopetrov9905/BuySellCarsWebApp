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
    public class CarOrderItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarOrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarOrderItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CarOrderItems.Include(c => c.Order).Include(c => c.Car);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarOrderItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarOrderItems == null)
            {
                return NotFound();
            }

            var carOrderItem = await _context.CarOrderItems
                .Include(c => c.Order)
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carOrderItem == null)
            {
                return NotFound();
            }

            return View(carOrderItem);
        }

        // GET: CarOrderItem/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            return View();
        }

        // POST: CarOrderItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Id,OrderId")] CarOrderItem carOrderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carOrderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carOrderItem.OrderId);
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carOrderItem.CarId);
            return View(carOrderItem);
        }

        // GET: CarOrderItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarOrderItems == null)
            {
                return NotFound();
            }

            var carOrderItem = await _context.CarOrderItems.FindAsync(id);
            if (carOrderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carOrderItem.OrderId);
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carOrderItem.CarId);
            return View(carOrderItem);
        }

        // POST: CarOrderItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Id,OrderId")] CarOrderItem carOrderItem)
        {
            if (id != carOrderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carOrderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarOrderItemExists(carOrderItem.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carOrderItem.OrderId);
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carOrderItem.CarId);
            return View(carOrderItem);
        }

        // GET: CarOrderItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarOrderItems == null)
            {
                return NotFound();
            }

            var carOrderItem = await _context.CarOrderItems
                .Include(c => c.Order)
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carOrderItem == null)
            {
                return NotFound();
            }

            return View(carOrderItem);
        }

        // POST: CarOrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarOrderItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarOrderItems'  is null.");
            }
            var carOrderItem = await _context.CarOrderItems.FindAsync(id);
            if (carOrderItem != null)
            {
                _context.CarOrderItems.Remove(carOrderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarOrderItemExists(int id)
        {
          return (_context.CarOrderItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
