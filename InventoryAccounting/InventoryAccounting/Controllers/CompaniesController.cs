﻿using System;
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
    public class CompaniesController : Controller
    {
        private readonly InventoryAccountingContext _context;

        public CompaniesController(InventoryAccountingContext context)
        {
            _context = context;
        }

        // GET: CompanyNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: CompanyNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyName = await _context.Companies
                .FirstOrDefaultAsync(m => m.Unp == id);
            if (companyName == null)
            {
                return NotFound();
            }

            return View(companyName);
        }

        // GET: CompanyNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies companyName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyName);
        }

        // GET: CompanyNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyName = await _context.Companies.FindAsync(id);
            if (companyName == null)
            {
                return NotFound();
            }
            return View(companyName);
        }

        // POST: CompanyNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies companyName)
        {
            if (id != companyName.Unp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyNameExists(companyName.Unp))
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
            return View(companyName);
        }

        // GET: CompanyNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyName = await _context.Companies
                .FirstOrDefaultAsync(m => m.Unp == id);
            if (companyName == null)
            {
                return NotFound();
            }

            return View(companyName);
        }

        // POST: CompanyNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyName = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(companyName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyNameExists(int id)
        {
            return _context.Companies.Any(e => e.Unp == id);
        }
    }
}
