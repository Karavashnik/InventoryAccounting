using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class ActsRepository : GenericDataRepository<Acts>, IActsRepository
    {
        public ActsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
