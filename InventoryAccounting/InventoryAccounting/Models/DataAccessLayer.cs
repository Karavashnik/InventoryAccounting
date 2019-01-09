using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class DataAccessLayer : ITmcDataAccessLayer, IActsDataAccessLayer, IPersonsDataAccessLayer, IContractsDataAccessLayer, IRoomsDataAccessLayer, ICompaniesDataAccessLayer
    {
        private readonly InventoryAccountingContext db;

        public DataAccessLayer(InventoryAccountingContext context)
        {
            db = context;
        }

        // Tmc's Methods
        public async Task<IEnumerable<Tmc>> GetAllTmc()
        {
            return await db.Tmc.Include(t => t.Act).Include(t => t.PesponsiblePersonNumberNavigation).Include(t => t.Room).ToListAsync();           
        }

        public async Task<Tmc> GetTmcById(int id)
        {
            return await db.Tmc
                .Include(t => t.Act)
                .Include(t => t.PesponsiblePersonNumberNavigation)
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.InventoryNumber == id);
        }

        public async void AddTmc(Tmc tmc)
        {
            db.Add(tmc);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdateTmc(Tmc tmc)
        {
            db.Update(tmc);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeleteTmc(Tmc tmc)
        {
            db.Tmc.Remove(tmc);
            await db.SaveChangesAsync();
        }
        public async void DeleteTmcById(int id)
        {
            db.Tmc.Remove(await db.Tmc.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> TmcExists(int id)
        {
            return await db.Tmc.AnyAsync(e => e.InventoryNumber == id);
        }

        // Act's methods
        public async Task<IEnumerable<Acts>> GetAllActs()
        {
            return await db.Acts.Include(a => a.ContractNumberNavigation).ToListAsync();
        }

        // Room's methods
        public async Task<IEnumerable<Rooms>> GetAllRooms()
        {
            return await db.Rooms.ToListAsync();
        }

        // Responsible Person's methods
        public async Task<IEnumerable<ResponsiblePersons>> GetAllPersons()
        {
            return await db.ResponsiblePersons.ToListAsync();
        }

        // Contract methods
        public async Task<IEnumerable<Contracts>> GetAllContracts()
        {
            return await db.Contracts.Include(c => c.CompanyUnpNavigation).ToListAsync();
        }

        // Company name's methods
        public async Task<IEnumerable<CompanyName>> GetAllCompanyNames()
        {
            return await db.CompanyName.ToListAsync();
        }
    }
}
