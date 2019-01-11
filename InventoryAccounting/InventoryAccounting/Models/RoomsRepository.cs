using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class RoomsRepository : GenericDataRepository<Rooms>, IRoomsRepository
    {
        public RoomsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
