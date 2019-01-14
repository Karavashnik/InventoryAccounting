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

namespace InventoryAccounting.Controllers
{
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
            return View( await _tmcs.GetAllAsync());
        }

        // GET: TmcController/Details/5
        [HttpGet("Tmc/Details/{id}")]
        [ValidateTmcExists]
        public IActionResult Details(Guid id)
        {
            var par = new Func<Tmc, bool>( tmc => tmc.Id == id); 
            return View(_tmcs.GetSingle(par));
        }

        // GET: TmcController/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["ActId"] = new SelectList(await _tmcs.GetAllActs(), "Id", "Id");
            //ViewData["PesponsiblePersonNumber"] = new SelectList(await _tmcs.GetAllPersons(), "PersonnelNumber", "FirstName");
           // ViewData["RoomId"] = new SelectList(await _tmcs.GetAllRooms(), "Id", "Name");
            return View();
        }

        // POST: TmcController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateModel]
        public async Task<IActionResult> Create([Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonNumber,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {
            if (ModelState.IsValid)
            {
                //_tmcs.AddTmc(tmc);
                return RedirectToAction(nameof(Index));
            }

            //ViewData["ActId"] = new SelectList(await _tmcs.GetAllActs(), "Id", "Id", tmc.ActId);
            //ViewData["PesponsiblePersonNumber"] = new SelectList(await _tmcs.GetAllPersons(), "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            //ViewData["RoomId"] = new SelectList(await _tmcs.GetAllRooms(), "Id", "Name", tmc.RoomId);
            return View(tmc);
        }

        // GET: TmcController/Edit/5
        [HttpGet("Tmc/Edit/{id}")]
        [ValidateTmcExists]
        public async Task<IActionResult> Edit(int id)
        {
            /*var tmc = await _tmcs.GetTmcById(id);
           
            if (tmc == null)
            {
                return NotFound();
            }
            ViewData["ActId"] = new SelectList(await _tmcs.GetAllActs(), "Id", "Id", tmc.ActId);
            ViewData["PesponsiblePersonNumber"] = new SelectList(await _tmcs.GetAllPersons(), "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            ViewData["RoomId"] = new SelectList(await _tmcs.GetAllRooms(), "Id", "Name", tmc.RoomId);
            return View(tmc);*/
            return View();
        }

        // POST: TmcController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateModel]
        public async Task<IActionResult> Edit(Guid id, [Bind("InventoryNumber,Name,Description,Type,PurchaseDate,PesponsiblePersonNumber,FactoryNumber,WriteOffDate,RoomId,ActId,WarrantyDate")] Tmc tmc)
        {/*
            if (id != tmc.InventoryNumber)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _tmcs.UpdateTmc(tmc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _tmcs.TmcExists(tmc.InventoryNumber))
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
            ViewData["ActId"] = new SelectList(await _tmcs.GetAllActs(), "Id", "Id", tmc.ActId);
            ViewData["PesponsiblePersonNumber"] = new SelectList(await _tmcs.GetAllPersons(), "PersonnelNumber", "FirstName", tmc.PesponsiblePersonNumber);
            ViewData["RoomId"] = new SelectList(await _tmcs.GetAllRooms(), "Id", "Name", tmc.RoomId);
            return View(tmc);
            */
            return View();
        }

        // GET: TmcController/Delete/5
        [HttpGet("Tmc/Delete/{id}")]
        [ValidateTmcExists]
        public async Task<IActionResult> Delete(int id)
        {
            return View(); //await _tmcs.GetTmcById(id));
        }

        // POST: TmcController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //_tmcs.DeleteTmcById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
