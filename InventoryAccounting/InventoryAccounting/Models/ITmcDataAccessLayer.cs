using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface ITmcDataAccessLayer
    {
        void AddTmc(Tmc tmc);
        Task<IEnumerable<Acts>> GetAllActs();
        Task<IEnumerable<ResponsiblePersons>> GetAllPersons();
        Task<IEnumerable<Rooms>> GetAllRooms();
        Task<IEnumerable<Tmc>> GetAllTmc();
        Task<Tmc> GetTmcById(int id);
        Task<bool> TmcExists(int id);
        Task<int> UpdateTmc(Tmc tmc);
        void DeleteTmc(Tmc tmc);
        void DeleteTmcById(int id);
    }
}