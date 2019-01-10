using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAccounting.Models.DB;

namespace InventoryAccounting.Models
{
    public interface IRoomsDataAccessLayer
    {
        Task<IEnumerable<Rooms>> GetAllRooms();
        Task<Rooms> GetRoomById(Guid id);
        void AddRoom(Rooms room);
        Task<int> UpdateRoom(Rooms room);
        void DeleteRoom(Rooms room);
        void DeleteRoomById(Guid id);
        Task<bool> RoomExists(Guid id);
    }
}