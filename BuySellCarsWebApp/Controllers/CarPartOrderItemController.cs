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
    public class CarPartOrderItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarPartOrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarPartOrderItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CarPartOrderItems.Include(c => c.Order).Include(c => c.CarPart);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarPartOrderItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarPartOrderItems == null)
            {
                return NotFound();
            }

            var carPartOrderItem = await _context.CarPartOrderItems
                .Include(c => c.Order)
                .Include(c => c.CarPart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPartOrderItem == null)
            {
                return NotFound();
            }

            return View(carPartOrderItem);
        }

        // GET: CarPartOrderItem/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["CarPartId"] = new SelectList(_context.CarParts, "Id", "Id");
            return View();
        }

        // POST: CarPartOrderItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarPartId,Id,OrderId")] CarPartOrderItem carPartOrderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carPartOrderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carPartOrderItem.OrderId);
            ViewData["CarPartId"] = new SelectList(_context.CarParts, "Id", "Id", carPartOrderItem.CarPartId);
            return View(carPartOrderItem);
        }

        // GET: CarPartOrderItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarPartOrderItems == null)
            {
                return NotFound();
            }

            var carPartOrderItem = await _context.CarPartOrderItems.FindAsync(id);
            if (carPartOrderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carPartOrderItem.OrderId);
            ViewData["CarPartId"] = new SelectList(_context.CarParts, "Id", "Id", carPartOrderItem.CarPartId);
            return View(carPartOrderItem);
        }

        // POST: CarPartOrderItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarPartId,Id,OrderId")] CarPartOrderItem carPartOrderItem)
        {
            if (id != carPartOrderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPartOrderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartOrderItemExists(carPartOrderItem.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", carPartOrderItem.OrderId);
            ViewData["CarPartId"] = new SelectList(_context.CarParts, "Id", "Id", carPartOrderItem.CarPartId);
            return View(carPartOrderItem);
        }

        // GET: CarPartOrderItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarPartOrderItems == null)
            {
                return NotFound();
            }

            var carPartOrderItem = await _context.CarPartOrderItems
                .Include(c => c.Order)
                .Include(c => c.CarPart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPartOrderItem == null)
            {
                return NotFound();
            }

            return View(carPartOrderItem);
        }

        // POST: CarPartOrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarPartOrderItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarPartOrderItems'  is null.");
            }
            var carPartOrderItem = await _context.CarPartOrderItems.FindAsync(id);
            if (carPartOrderItem != null)
            {
                _context.CarPartOrderItems.Remove(carPartOrderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPartOrderItemExists(int id)
        {
          return (_context.CarPartOrderItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
