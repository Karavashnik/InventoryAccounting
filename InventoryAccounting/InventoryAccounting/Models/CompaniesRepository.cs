using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class CompaniesRepository : GenericDataRepository<CompanyName>, ICompaniesRepository
    {
        public CompaniesRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
