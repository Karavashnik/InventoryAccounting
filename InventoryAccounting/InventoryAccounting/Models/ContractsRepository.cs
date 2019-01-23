using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class ContractsRepository : GenericDataRepository<Contracts>, IContractsRepository
    {
        public ContractsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
