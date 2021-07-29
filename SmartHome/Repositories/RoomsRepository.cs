using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Repositories
{
    public interface IRoomsRepository
    {
        Room AddRoom(AddRoomViewModel model);
        Room GetRoom(Guid id);
        IEnumerable<Room> GetAllRooms();
        Room UpdateRoom(Guid id, UpdateRoomViewModel model);
        void DeleteRoom(Guid id);
    }

    public class RoomsRepository : IRoomsRepository
    {
        public Room AddRoom(AddRoomViewModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public Room GetRoom(Guid id)
        {
            throw new NotImplementedException();
        }

        public Room UpdateRoom(Guid id, UpdateRoomViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
