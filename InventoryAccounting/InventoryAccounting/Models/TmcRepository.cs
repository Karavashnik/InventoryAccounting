using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class TmcRepository : GenericDataRepository<Tmc>, ITmcRepository
    {
        public TmcRepository(InventoryAccountingContext context) : base(context)
        {   
        }
    }
}
