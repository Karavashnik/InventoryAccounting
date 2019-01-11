using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class CompaniesRepository : GenericDataRepository<Companies>, ICompaniesRepository
    {
        public CompaniesRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
