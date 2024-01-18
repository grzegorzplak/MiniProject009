using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProject009.Models;

namespace MiniProject009.Controllers
{
    public class ExpendituresController : Controller
    {
        private readonly Context _context;

        public ExpendituresController(Context context)
        {
            _context = context;
        }

        // GET: Expenditures
        public async Task<IActionResult> Index()
        {
            var context = _context.Expenditures.Include(e => e.Category);
            return View(await context.ToListAsync());
        }

        // GET: Expenditures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // GET: Expenditures/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Expenditures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenditureDate,ExpenditureName,ExpenditureAmount,CategoryId")] Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenditure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", expenditure.CategoryId);
            return View(expenditure);
        }

        // GET: Expenditures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", expenditure.CategoryId);
            return View(expenditure);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenditureDate,ExpenditureName,ExpenditureAmount,CategoryId")] Expenditure expenditure)
        {
            if (id != expenditure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenditure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenditureExists(expenditure.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", expenditure.CategoryId);
            return View(expenditure);
        }

        // GET: Expenditures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure != null)
            {
                _context.Expenditures.Remove(expenditure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenditureExists(int id)
        {
            return _context.Expenditures.Any(e => e.Id == id);
        }
    }
}
