using System;
using System.Threading.Tasks;
using InventoryAccounting.Filters;
using Microsoft.AspNetCore.Mvc;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Controllers
{
    [Authorize]
    public class TmcTypesController : Controller
    {
        private ITmcTypesRepository _types;

        public TmcTypesController(InventoryAccountingContext context)
        {
            _types = new TmcTypesRepository(context);
        }
        // GET: TmcTypesController/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: TmcTypesController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TmcTypes types)
        {
            await _types.AddAsync(types);
            return PartialView(types);    
        }

        // GET: TmcTypesController/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<TmcTypes>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView("Create", await _types.GetSingleAsync(tmc => tmc.Id == id));
        }

        // POST: TmcTypesController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] TmcTypes types)
        {
            try
            {
                await _types.UpdateAsync(types);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _types.ItemExists(x=>x.Id == types.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return PartialView("Create", types);  
        }

        // GET: TmcTypesController/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<TmcTypes>))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return PartialView(await _types.GetSingleAsync(x=>x.Id == id));
        }

        // POST: TmcTypesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var types = await _types.GetSingleAsync(x=> x.Id == id);
            try
            {
                await _types.RemoveAsync(types);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Данную запись невозможно удалить, потому что она связана с другими записями.");
                return PartialView("Delete", types);
            }
            return PartialView("Delete");
        }
        [HttpPost]
        [HttpGet]
        public async Task<JsonResult> GetTmcTypes()
        {
            return Json(await _types.GetAllAsync());
        }
    }
}
