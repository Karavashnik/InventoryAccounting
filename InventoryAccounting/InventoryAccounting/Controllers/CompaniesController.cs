using System;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            var companies = await _companies.GetAllAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_Table", companies);
            }
            return View(companies);
        }

        // GET: Companies/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Details(Guid? id)
        {
            return PartialView("Details", await _companies.GetSingleAsync(x => x.Id == id));
        }
        // GET: Companies/Create
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: CompanyNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        //[ValidateCompaniesAttribute]
        public async Task<IActionResult> Create([Bind("Id,Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies company)
        {
            await _companies.AddAsync(company);
            return PartialView("Create", company);
        }

        // GET: Companies/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView("Create", await _companies.GetSingleAsync(x => x.Id == id));
        }

        // POST: CompanyNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Unp,Name,Address,DirectorsName,DirectorsPhone")] Companies companies)
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
            return PartialView("Create", companies);
        }

        // GET: Companies/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Companies>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView("Delete", await _companies.GetSingleAsync(x => x.Id == id));
        }

        // POST: CompanyNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var company = await _companies.GetSingleAsync(x=> x.Id == id);
            try
            {
                await _companies.RemoveAsync(company);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("Id", "Данную запись невозможно удалить, потому что она связана с другими записями.");
                return PartialView("Delete", company);
            }
            return PartialView("Delete");
        }
        [HttpPost]
        [HttpGet]
        public async Task<JsonResult> GetCompanies()
        {
            return Json(await _companies.GetAllAsync());
        }
        
        public async Task<JsonResult> IsUnpUnique(int Unp)
        {
            var validateUnp = await _companies.GetSingleAsync(x => x.Unp == Unp);
            return Json(validateUnp == null);
        }
    }
}
