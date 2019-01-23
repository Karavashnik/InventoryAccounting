using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ActsController : Controller
    {
        private IActsRepository _acts;
        public ActsController(InventoryAccountingContext context)
        {
            _acts = new ActsRepository(context);
        }

        // GET: Acts
        public async Task<IActionResult> Index()
        {
            var acts = await _acts.GetAllAsync(x => x.Contract);
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_Table", acts);
            }
            return View(acts);
        }

        // GET: Acts/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Acts>))]
        public async Task<IActionResult> Details(Guid id)
        {
            return PartialView(await _acts.GetSingleAsync(act => act.Id == id, act => act.Contract));
        }

        // GET: Acts/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["CompanyId"] = await GetContracts();
            return View();
        }

        // POST: Acts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContractId, ActNumber, Type, CompilationDate")] Acts acts)
        {
            await _acts.AddAsync(acts);
            return RedirectToAction(nameof(Index));
        }

        // GET: Acts/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Acts>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            //ViewData["ContractId"] = new SelectList(await _acts.GetAllContractsAsync(), "Id", "ContractNumber");
            return View(await _acts.GetSingleAsync(act => act.Id == id));
        }

        // POST: Acts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,ContractId, ActNumber, Type, CompilationDate")] Acts acts)
        {
            try
            {
                await _acts.UpdateAsync(acts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _acts.ItemExists(x=> x.Id == acts.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Acts/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Acts>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView(await _acts.GetSingleAsync(act => act.Id == id));
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _acts.RemoveAsync(await _acts.GetSingleAsync(act=> act.Id == id));
            return PartialView("Delete");
        }
        
        public ActionResult CreateModal()
        {
            return PartialView("CreateModal");
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModal([Bind("Id,ContractId, ActNumber, Type, CompilationDate")] Acts acts)
        {
            await _acts.AddAsync(acts);
            return PartialView("CreateModal", acts);
        }
        [HttpPost]
        [HttpGet]
        public async Task<JsonResult> GetActs()
        {
            return Json(await _acts.GetAllAsync());
        }
    }
}
