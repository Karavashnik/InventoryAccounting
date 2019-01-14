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
    public class PersonsController : Controller
    {
        private readonly InventoryAccountingContext _context;

        public PersonsController(InventoryAccountingContext context)
        {
            _context = context;
        }

        // GET: ResponsiblePersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: ResponsiblePersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsiblePersons = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonnelNumber == id);
            if (responsiblePersons == null)
            {
                return NotFound();
            }

            return View(responsiblePersons);
        }

        // GET: ResponsiblePersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResponsiblePersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelNumber,LastName,FirstName,MiddleName,DateOfBirth,PassportDetails,Education,DateOfEmployment,Phone,Email,Post")] Persons responsiblePersons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsiblePersons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsiblePersons);
        }

        // GET: ResponsiblePersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsiblePersons = await _context.Persons.FindAsync(id);
            if (responsiblePersons == null)
            {
                return NotFound();
            }
            return View(responsiblePersons);
        }

        // POST: ResponsiblePersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelNumber,LastName,FirstName,MiddleName,DateOfBirth,PassportDetails,Education,DateOfEmployment,Phone,Email,Post")] Persons responsiblePersons)
        {
            if (id != responsiblePersons.PersonnelNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsiblePersons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsiblePersonsExists(responsiblePersons.PersonnelNumber))
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
            return View(responsiblePersons);
        }

        // GET: ResponsiblePersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsiblePersons = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonnelNumber == id);
            if (responsiblePersons == null)
            {
                return NotFound();
            }

            return View(responsiblePersons);
        }

        // POST: ResponsiblePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsiblePersons = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(responsiblePersons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsiblePersonsExists(int id)
        {
            return _context.Persons.Any(e => e.PersonnelNumber == id);
        }
    }
}
