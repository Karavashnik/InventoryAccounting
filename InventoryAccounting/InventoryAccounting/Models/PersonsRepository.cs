using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class PersonsRepository : GenericDataRepository<Persons>, IPersonsRepository
    {
        public PersonsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
