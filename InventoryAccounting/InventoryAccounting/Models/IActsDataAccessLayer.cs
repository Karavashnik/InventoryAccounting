using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IActsDataAccessLayer
    {
        Task<IEnumerable<Acts>> GetAllActs();
    }
}