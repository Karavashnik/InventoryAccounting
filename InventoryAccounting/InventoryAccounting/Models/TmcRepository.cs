using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public class TmcRepository : GenericDataRepository<Tmc>, ITmcRepository
    {
        public TmcRepository(InventoryAccountingContext context) : base(context)
        {   
        }

        public async Task<IList<Persons>> GetPersonsAsync()
        {
            return await context.Persons.ToListAsync();
        }

        public async Task<IList<Rooms>> GetRoomsAsync()
        {
            return await context.Rooms.ToListAsync();
        }

        public async Task<IList<Acts>> GetActsAsync()
        {
            return await context.Acts.ToListAsync();
        }
    }
}
