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
    public class TmcController : Controller
    {
        private ITmcRepository _tmcs;

        //private readonly InventoryAccountingContext _context;
        public TmcController(InventoryAccountingContext context)
        {
            _tmcs = new TmcRepository(context);
        }

        // GET: TmcController
        public async Task<IActionResult> Index()
        {
            return View( await _tmcs.GetAllAsync(x=> x.ResponsiblePerson, x=>x.Room, x=>x.Act));
        }

        // GET: TmcController/Details/5
        [HttpGet("Tmc/Details/{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Tmc>))]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _tmcs.GetSingleAsync(tmc=> tmc.Id == id, x=>x.ResponsiblePerson, x=>x.Room, x=>x.Act));
        }

        // GET: TmcController/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ActId"] = new SelectList(await _tmcs.GetActsAsync(), "Id", "ActNumber");
            ViewData["PesponsiblePersonId"] = new SelectList(await _tmcs.GetPersonsAsync(), "Id", "FirstName" + "LastName");
            ViewData["RoomId"] = new SelectList(await _tmcs.GetRoomsAsync(), "Id", "Name");
            return View();
        }

        // POST: TmcController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonId,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {
            await _tmcs.AddAsync(tmc);
            return RedirectToAction(nameof(Index));
        }

        // GET: TmcController/Edit/5
        [HttpGet("Tmc/Edit/{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Tmc>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["ActId"] = new SelectList(await _tmcs.GetActsAsync(), "Id", "ActNumber");
            ViewData["PesponsiblePersonId"] = new SelectList(await _tmcs.GetPersonsAsync(), "Id", "FirstName" + "LastName");
            ViewData["RoomId"] = new SelectList(await _tmcs.GetRoomsAsync(), "Id", "Name");
            return View(await _tmcs.GetSingleAsync(tmc => tmc.Id == id, x => x.ResponsiblePerson, x => x.Room, x => x.Act));
        }

        // POST: TmcController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonId,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {
            try
            {
                await _tmcs.UpdateAsync(tmc);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _tmcs.ItemExists(x=>x.Id == tmc.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TmcController/Delete/5
        [HttpGet("Tmc/Delete/{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Tmc>))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _tmcs.GetSingleAsync(x=>x.Id == id));
        }

        // POST: TmcController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _tmcs.RemoveAsync(await _tmcs.GetSingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}
