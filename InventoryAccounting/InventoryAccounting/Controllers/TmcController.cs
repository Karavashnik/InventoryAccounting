using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Controllers
{
    public class TmcController : Controller
    {
        private readonly InventoryAccountingContext _context;

        public TmcController(InventoryAccountingContext context)
        {
            _context = context;
        }

        // GET: TmcController
        public async Task<IActionResult> Index()
        {
            var inventoryAccountingContext = _context.Tmc.Include(t => t.Act).Include(t => t.PesponsiblePersonNumberNavigation).Include(t => t.Room);
            return View(await inventoryAccountingContext.ToListAsync());
        }

        // GET: TmcController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tmc = await _context.Tmc
                .Include(t => t.Act)
                .Include(t => t.PesponsiblePersonNumberNavigation)
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.InventoryNumber == id);
            if (tmc == null)
            {
                return NotFound();
            }

            return View(tmc);
        }

        // GET: TmcController/Create
        public IActionResult Create()
        {
            ViewData["ActId"] = new SelectList(_context.Acts, "Id", "Id");
            ViewData["PesponsiblePersonNumber"] = new SelectList(_context.ResponsiblePersons, "PersonnelNumber", "FirstName");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }

        // POST: TmcController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonNumber,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tmc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActId"] = new SelectList(_context.Acts, "Id", "Id", tmc.ActId);
            ViewData["PesponsiblePersonNumber"] = new SelectList(_context.ResponsiblePersons, "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", tmc.RoomId);
            return View(tmc);
        }

        // GET: TmcController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tmc = await _context.Tmc.FindAsync(id);
            if (tmc == null)
            {
                return NotFound();
            }
            ViewData["ActId"] = new SelectList(_context.Acts, "Id", "Id", tmc.ActId);
            ViewData["PesponsiblePersonNumber"] = new SelectList(_context.ResponsiblePersons, "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", tmc.RoomId);
            return View(tmc);
        }

        // POST: TmcController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonNumber,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {
            if (id != tmc.InventoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tmc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TmcExists(tmc.InventoryNumber))
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
            ViewData["ActId"] = new SelectList(_context.Acts, "Id", "Id", tmc.ActId);
            ViewData["PesponsiblePersonNumber"] = new SelectList(_context.ResponsiblePersons, "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", tmc.RoomId);
            return View(tmc);
        }

        // GET: TmcController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tmc = await _context.Tmc
                .Include(t => t.Act)
                .Include(t => t.PesponsiblePersonNumberNavigation)
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.InventoryNumber == id);
            if (tmc == null)
            {
                return NotFound();
            }

            return View(tmc);
        }

        // POST: TmcController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tmc = await _context.Tmc.FindAsync(id);
            _context.Tmc.Remove(tmc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TmcExists(int id)
        {
            return _context.Tmc.Any(e => e.InventoryNumber == id);
        }
    }
}
