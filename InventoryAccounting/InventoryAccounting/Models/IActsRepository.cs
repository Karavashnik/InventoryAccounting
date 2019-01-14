using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IActsRepository : IGenericDataRepository<Acts>
    {
        Task<IList<Contracts>> GetAllContractsAsync();
    }
}
