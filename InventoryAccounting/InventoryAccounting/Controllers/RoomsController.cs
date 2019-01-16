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
    public class RoomsController : Controller
    {
        private readonly IRoomsRepository _rooms;
        public RoomsController(InventoryAccountingContext context)
        {
            _rooms = new RoomsRepository(context);
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _rooms.GetAllAsync());
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Floor,Number,Phone")] Rooms rooms)
        {
            await _rooms.AddAsync(rooms);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Rooms>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView(await _rooms.GetSingleAsync(x => x.Id == id));
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Floor,Number,Phone")] Rooms rooms)
        {
            try
            {
                await _rooms.UpdateAsync(rooms);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _rooms.ItemExists(x => x.Id == rooms.Id))
                {
                    return NotFound();
                }
                throw;
            }
            
            return PartialView(await _rooms.GetSingleAsync(x => x.Id == rooms.Id));
            
        }
        // GET: Rooms/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Rooms>))]
        public async Task<IActionResult> Details(Guid? id)
        {
            return PartialView("DetailsModal", await _rooms.GetSingleAsync(x => x.Id == id));
        }
        // GET: Rooms/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Rooms>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView("DeleteModal", await _rooms.GetSingleAsync(x => x.Id == id));
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _rooms.RemoveAsync(await _rooms.GetSingleAsync(x => x.Id == id));
            return RedirectToAction(nameof(Index));
        }
    }
}
