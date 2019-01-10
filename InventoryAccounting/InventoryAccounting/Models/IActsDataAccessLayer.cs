using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IActsDataAccessLayer
    {
        Task<IEnumerable<Acts>> GetAllActs();
        Task<Acts> GetActById(int id);
        void AddAct(Acts act);
        Task<int> UpdateAct(Acts act);
        void DeleteAct(Acts act);
        void DeleteActById(int id);
        Task<bool> ActExists(int id);

    }
}