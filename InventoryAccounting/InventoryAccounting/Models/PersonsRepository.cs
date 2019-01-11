using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public class PersonsRepository : GenericDataRepository<ResponsiblePersons>, IPersonsRepository
    {
        public PersonsRepository(InventoryAccountingContext context) : base(context)
        {
        }
    }
}
