using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class ActsRepository : GenericDataRepository<Acts>, IActsRepository
    {
        public ActsRepository(InventoryAccountingContext context) : base(context)
        {
        }

        //public async override Task<IList<Acts>> GetAllAsync()
        //{
        //   return await context.Acts.Include(acts => acts.ContractId).ToListAsync();
        //}

        public async Task<IList<Contracts>> GetAllContractsAsync()
        {
            return await context.Contracts.ToListAsync();
        }
    }
}
