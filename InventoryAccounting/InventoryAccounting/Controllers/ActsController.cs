using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace InventoryAccounting.Controllers
{
    [Authorize]
    public class ActsController : Controller
    {
        private readonly InventoryAccountingContext _context;

        public ActsController(InventoryAccountingContext context)
        {
            _context = context;
        }

        // GET: Acts
        public async Task<IActionResult> Index()
        {
            var inventoryAccountingContext = _context.Acts.Include(a => a.ContractId);
            return View(await inventoryAccountingContext.ToListAsync());
        }

        // GET: Acts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acts = await _context.Acts
                .Include(a => a.ContractId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acts == null)
            {
                return NotFound();
            }

            return View(acts);
        }

        // GET: Acts/Create
        public IActionResult Create()
        {
            ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber");
            return View();
        }

        // POST: Acts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompilationDate,ContractNumber")] Acts acts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber", acts.ContractId);
            return View(acts);
        }

        // GET: Acts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acts = await _context.Acts.FindAsync(id);
            if (acts == null)
            {
                return NotFound();
            }
            ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber", acts.ContractId);
            return View(acts);
        }

        // POST: Acts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CompilationDate,ContractNumber")] Acts acts)
        {
            if (id != acts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActsExists(acts.Id))
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
            ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber", acts.ContractId);
            return View(acts);
        }

        // GET: Acts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acts = await _context.Acts
                .Include(a => a.ContractId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acts == null)
            {
                return NotFound();
            }

            return View(acts);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acts = await _context.Acts.FindAsync(id);
            _context.Acts.Remove(acts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public ActionResult CreateModal()
        {
            ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber");
            return PartialView("CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void CreateModal([Bind("Id,CompilationDate,ContractNumber")] Acts acts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acts);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["ContractNumber"] = new SelectList(_context.Contracts, "ContractNumber", "ContractNumber", acts.ContractNumber);
            //return View(acts);
        }

        private bool ActsExists(Guid id)
        {
            return _context.Acts.Any(e => e.Id == id);
        }
    }
}
