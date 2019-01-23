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
    }
}
