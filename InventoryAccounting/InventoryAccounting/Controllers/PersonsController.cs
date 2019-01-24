using System;
using System.Linq;
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
    public class PersonsController : Controller
    {
        private IPersonsRepository _persons;
        public PersonsController(InventoryAccountingContext context)
        {
            _persons = new PersonsRepository(context);
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var persons = await _persons.GetAllAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_Table", persons);
            }
            return View(persons);
        }

        // GET: Persons/Details/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Persons>))]
        public async Task<IActionResult> Details(Guid? id)
        {
            return PartialView(await _persons.GetSingleAsync(x=>x.Id == id));
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonnelNumber,LastName,FirstName,MiddleName,DateOfBirth,PassportDetails,Education,DateOfEmployment,Phone,Email,Post")] Persons person)
        {
            await _persons.AddAsync(person);
            return PartialView("Create", person);
        }

        // GET: Persons/Edit/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Persons>))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return PartialView("Create", await _persons.GetSingleAsync(x=>x.Id == id));
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,PersonnelNumber,LastName,FirstName,MiddleName,DateOfBirth,PassportDetails,Education,DateOfEmployment,Phone,Email,Post")] Persons person)
        {
            try
            {
                await _persons.UpdateAsync(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _persons.ItemExists(x=>x.Id == person.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return PartialView("Create", person);
        }

        // GET: Persons/Delete/5
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Persons>))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return PartialView(await _persons.GetSingleAsync(x=>x.Id == id));
        }

        // POST: ResponsiblePersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _persons.RemoveAsync(await _persons.GetSingleAsync(x => x.Id == id));
            return PartialView("Delete");
        }
        [HttpPost]
        [HttpGet]
        public async Task<JsonResult> GetPersons()
        {
            return Json(await _persons.GetAllAsync());
        }
    }
}
