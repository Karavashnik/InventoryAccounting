using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface ICompaniesDataAccessLayer
    {
        Task<IEnumerable<CompanyName>> GetAllCompanyNames();
        Task<CompanyName> GetCompanyById(int id);
        void AddCompany(CompanyName company);
        Task<int> UpdateCompany(CompanyName company);
        void DeleteCompany(CompanyName company);
        void DeleteCompanyById(int id);
        Task<bool> CompanyExists(int id);
    }
}