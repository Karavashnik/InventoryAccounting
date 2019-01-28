using InventoryAccounting.Models.DB;
namespace InventoryAccounting.Models
{
    public class TmcTypesRepository : GenericDataRepository<TmcTypes>, ITmcTypesRepository
    {
        public TmcTypesRepository(InventoryAccountingContext context) : base(context)
        {   
        }
    }
}
