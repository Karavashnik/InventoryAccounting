using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IPersonsDataAccessLayer
    {
        Task<IEnumerable<ResponsiblePersons>> GetAllPersons();
    }
}