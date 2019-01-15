using System;
using System.Threading.Tasks;
using InventoryAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Filters
{
    public class ValidateCompaniesExistsAttribute : TypeFilterAttribute
    {
        public ValidateCompaniesExistsAttribute() : base(typeof(ValidateCompaniesExistsAttribute)) { }

        private class ValidateCompaniesExistsFilterImpl : IAsyncActionFilter
        {
            private readonly InventoryAccountingContext _context;

            public ValidateCompaniesExistsFilterImpl(InventoryAccountingContext context)
            {
                _context = context;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    if (context.ActionArguments["id"] is Guid id)
                    {
                        if (await _context.Companies.AllAsync(t => t.Id != id))
                        {
                            context.Result = new NotFoundObjectResult(id);
                            return;
                        }
                    }
                }

                await next();
            }
        }
    }
}
