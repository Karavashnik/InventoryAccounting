using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class TmcRepository : GenericDataRepository<Tmc>, ITmcRepository
    {
        public TmcRepository(InventoryAccountingContext context) : base(context)
        {   
        }
    }
}
