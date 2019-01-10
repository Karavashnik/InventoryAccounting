using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IPersonsDataAccessLayer
    {
        Task<IEnumerable<ResponsiblePersons>> GetAllPersons();
        Task<ResponsiblePersons> GetPersonById(int id);

        void AddPerson(ResponsiblePersons person);
        Task<int> UpdatePerson(ResponsiblePersons person);

        void DeletePerson(ResponsiblePersons person);
        void DeletePersonById(int id);

        Task<bool> PersonExists(int id);
    }
}