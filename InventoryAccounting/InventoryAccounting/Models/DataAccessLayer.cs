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
        public async Task<Acts> GetActById(int id)
        {
            return await db.Acts
                .Include(a => a.ContractNumberNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async void AddAct(Acts act)
        {
            db.Add(act);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdateAct(Acts act)
        {
            db.Update(act);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeleteAct(Acts act)
        {
            db.Acts.Remove(act);
            await db.SaveChangesAsync();
        }
        public async void DeleteActById(int id)
        {
            db.Acts.Remove(await db.Acts.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> ActExists(int id)
        {
            return await db.Acts.AnyAsync(e => e.Id == id);
        }

        // Room's methods
        public async Task<IEnumerable<Rooms>> GetAllRooms()
        {
            return await db.Rooms.ToListAsync();
        }
        public async Task<Rooms> GetRoomById(Guid id)
        {
            return await db.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async void AddRoom(Rooms room)
        {
            db.Add(room);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdateRoom(Rooms room)
        {
            db.Update(room);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeleteRoom(Rooms room)
        {
            db.Rooms.Remove(room);
            await db.SaveChangesAsync();
        }
        public async void DeleteRoomById(Guid id)
        {
            db.Rooms.Remove(await db.Rooms.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> RoomExists(Guid id)
        {
            return await db.Rooms.AnyAsync(e => e.Id == id);
        }


        // Responsible Person's methods
        public async Task<IEnumerable<ResponsiblePersons>> GetAllPersons()
        {
            return await db.ResponsiblePersons.ToListAsync();
        }
        public async Task<ResponsiblePersons> GetPersonById(int id)
        {
            return await db.ResponsiblePersons
                .FirstOrDefaultAsync(m => m.PersonnelNumber == id);
        }

        public async void AddPerson(ResponsiblePersons person)
        {
            db.Add(person);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdatePerson(ResponsiblePersons person)
        {
            db.Update(person);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeletePerson(ResponsiblePersons person)
        {
            db.ResponsiblePersons.Remove(person);
            await db.SaveChangesAsync();
        }
        public async void DeletePersonById(int id)
        {
            db.ResponsiblePersons.Remove(await db.ResponsiblePersons.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> PersonExists(int id)
        {
            return await db.ResponsiblePersons.AnyAsync(e => e.PersonnelNumber == id);
        }
        // Contract methods
        public async Task<IEnumerable<Contracts>> GetAllContracts()
        {
            return await db.Contracts.Include(c => c.CompanyUnpNavigation).ToListAsync();
        }
        public async Task<Contracts> GetContractById(int id)
        {
            return await db.Contracts
                .Include(c => c.CompanyUnpNavigation)
                .FirstOrDefaultAsync(m => m.ContractNumber == id);
        }

        public async void AddContract(Contracts contract)
        {
            db.Add(contract);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdateContract(Contracts contract)
        {
            db.Update(contract);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeleteContract(Contracts contract)
        {
            db.Contracts.Remove(contract);
            await db.SaveChangesAsync();
        }
        public async void DeleteContractById(int id)
        {
            db.Contracts.Remove(await db.Contracts.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> ContractExists(int id)
        {
            return await db.Contracts.AnyAsync(e => e.ContractNumber == id);
        }

        // Company name's methods
        public async Task<IEnumerable<CompanyName>> GetAllCompanyNames()
        {
            return await db.CompanyName.ToListAsync();
        }
        public async Task<CompanyName> GetCompanyById(int id)
        {
            return await db.CompanyName
                .FirstOrDefaultAsync(m => m.Unp == id);
        }

        public async void AddCompany(CompanyName company)
        {
            db.Add(company);
            await db.SaveChangesAsync();
        }
        public async Task<int> UpdateCompany(CompanyName company)
        {
            db.Update(company);
            await db.SaveChangesAsync();

            return 1;
        }

        public async void DeleteCompany(CompanyName company)
        {
            db.CompanyName.Remove(company);
            await db.SaveChangesAsync();
        }
        public async void DeleteCompanyById(int id)
        {
            db.CompanyName.Remove(await db.CompanyName.FindAsync(id));
            await db.SaveChangesAsync();
        }

        public async Task<bool> CompanyExists(int id)
        {
            return await db.CompanyName.AnyAsync(e => e.Unp == id);
        }
    }
}
