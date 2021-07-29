using Microsoft.Extensions.Configuration;
using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
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
        private IConfiguration _config;
        private string _connString;

        public RoomsRepository(IConfiguration config)
        {
            _config = config;
            _connString = _config.GetConnectionString("Local");
        }
        public Room AddRoom(AddRoomViewModel model)
        {
            using (var db = new SqlConnection(_connString))
            {
                var id = Guid.NewGuid();
                var result = db.Execute("INSERT INTO Rooms (Id, CreatedOn, Name) VALUES (@Id, @CreatedOn, @Name)", new { Id = id, CreatedOn = DateTime.UtcNow, Name = model.Name });
                if (result > 0)
                {
                    return GetRoom(id);
                }
                return null;
            }
        }

        public void DeleteRoom(Guid id)
        {
            using(var db = new SqlConnection(_connString))
            {
                db.Execute($"DELETE FROM Rooms WHERE Id = '{id}'");
            }
        }

        public IEnumerable<Room> GetAllRooms()
        {
            using (var db = new SqlConnection(_connString))
            {
                var rooms = db.Query<Room>($"SELECT * FROM ROOMS");
                return rooms;
            }
        }

        public Room GetRoom(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {
                var rooms = db.Query<Room>($"SELECT * FROM Rooms WHERE Id = '{ id }'");
                var room = rooms.FirstOrDefault();
                return room;
            }
        }

        public Room UpdateRoom(Guid id, UpdateRoomViewModel model)
        {
            using (var db = new SqlConnection(_connString))
            {
                var result = db.Execute($"UPDATE Rooms SET Name = CASE WHEN @Name IS NULL THEN Name ELSE @NAME END WHERE id = '{id}'", model);
                if (result > 0)
                {
                    return GetRoom(id);
                }
                return null; 
            }
        }
    }
}
