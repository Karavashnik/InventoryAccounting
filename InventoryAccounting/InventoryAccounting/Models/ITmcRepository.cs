using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface ITmcRepository : IGenericDataRepository<Tmc>
    {
        Task<IList<Persons>> GetPersonsAsync();
        Task<IList<Rooms>> GetRoomsAsync();
        Task<IList<Acts>> GetActsAsync();
    }
}
