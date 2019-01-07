using System.Threading.Tasks;
using InventoryAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Filters
{
    public class ValidateTmcExistsAttribute : TypeFilterAttribute
    {
        public ValidateTmcExistsAttribute() : base(typeof(ValidateTmcExistsAttribute)) {}

        private class ValidateTmcExistsFilterImpl : IAsyncActionFilter
        {
            private readonly InventoryAccountingContext _context;

            public ValidateTmcExistsFilterImpl(InventoryAccountingContext context)
            {
                _context = context;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    if (context.ActionArguments["id"] is int id)
                    {
                        if (await _context.Tmc.AllAsync(t => t.InventoryNumber != (int?) id))
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
