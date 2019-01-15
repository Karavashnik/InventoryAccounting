using System;
using System.Threading.Tasks;
using InventoryAccounting.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace InventoryAccounting.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private IContractsRepository _contracts;
        public ContractsController(InventoryAccountingContext context)
        {
            _contracts = new ContractsRepository(context);
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            return View(await _contracts.GetAllAsync(x => x.CompanyId));
        }

        // GET: Contracts/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Contracts>))]
        public async Task<IActionResult> Details(Guid? id)
        {
            return View(await _contracts.GetSingleAsync(x => x.Id == id, c => c.CompanyId));
        }

        // GET: Contracts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CompanyId"] = new SelectList(await _contracts.GetCompaniesAsync(), "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,ContractNumber,Type,ExpirationDate,CompilationDate")] Contracts contracts)
        {
            await _contracts.AddAsync(contracts);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Contracts>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["CompanyId"] = new SelectList(await _contracts.GetCompaniesAsync(), "Id", "Name");
            return View(await _contracts.GetSingleAsync(x=>x.Id == id));
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CompanyId,ContractNumber,Type,ExpirationDate,CompilationDate")] Contracts contracts)
        {
            try
            {
                await _contracts.UpdateAsync(contracts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _contracts.ItemExists(x=>x.Id == contracts.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Contracts>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View(await _contracts.GetSingleAsync(x=>x.Id == id, x=>x.CompanyId));
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _contracts.RemoveAsync(await _contracts.GetSingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}
