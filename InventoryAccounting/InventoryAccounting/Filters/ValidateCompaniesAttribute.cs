using System.Linq;
using System.Reflection;
using InventoryAccounting.Models;
using InventoryAccounting.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryAccounting.Filters
{
    public class ValidateCompaniesAttribute : ActionFilterAttribute
    {
        private readonly InventoryAccountingContext _context;
        public ValidateCompaniesAttribute(InventoryAccountingContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var model = context.ActionArguments?.Count > 0
                ? context.ActionArguments.First().Value as Companies
                : null;
            var company = _context.Companies.FirstOrDefault(x=>x.Unp == model.Unp);
            if (company != null)
            {
                context.ModelState.AddModelError("Unp", "Такой УНП уже существует.");
                context.Result = (IActionResult)controller?.View((context.ActionDescriptor as ControllerActionDescriptor)?.ActionName, model)
                                 ?? new BadRequestResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
