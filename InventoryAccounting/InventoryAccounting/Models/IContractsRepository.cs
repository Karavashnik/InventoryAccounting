using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IContractsRepository : IGenericDataRepository<Contracts>
    {
        Task<IList<Companies>> GetCompaniesAsync();
    }
}
