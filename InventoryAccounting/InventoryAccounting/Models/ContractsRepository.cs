using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class ContractsRepository : GenericDataRepository<Contracts>, IContractsRepository
    {
        public ContractsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
