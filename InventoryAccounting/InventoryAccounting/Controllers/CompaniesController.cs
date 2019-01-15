using System;
using System.Threading.Tasks;
using InventoryAccounting.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace InventoryAccounting.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private ICompaniesRepository _companies;
        public CompaniesController(InventoryAccountingContext context)
        {
            _companies = new CompaniesRepository(context);
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _companies.GetAllAsync());
        }

        // GET: Companies/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(await _companies.GetSingleAsync(x => x.Id == id));
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies company)
        {
            await _companies.AddAsync(company);
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return View(await _companies.GetSingleAsync(x => x.Id == id));
        }

        // POST: CompanyNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies companies)
        {
            try
            {
                await _companies.UpdateAsync(companies);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _companies.ItemExists(x => x.Id == companies.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Companies/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View(await _companies.GetSingleAsync(x => x.Id == id));
        }

        // POST: CompanyNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _companies.RemoveAsync(await _companies.GetSingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}
