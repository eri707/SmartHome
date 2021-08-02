using Dapper;
using Microsoft.Extensions.Configuration;
using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Repositories
{
    public interface IDevicesRepository
    {
        Device AddDevice(AddDeviceViewModel model);
        Device GetDevice(Guid id);
        IEnumerable<Device> GetAllDevices(Guid roomId);
        Device UpdateDevice(UpdateDeviceViewModel model, Guid id);
        void DeleteDevice(Guid id);

    }
    public class DevicesRepository : IDevicesRepository
    {
        private IConfiguration _config;
        private string _connString;

        public DevicesRepository(IConfiguration config)
        {
            _config = config;
            _connString = _config.GetConnectionString("Local");
        }
        public Device AddDevice(AddDeviceViewModel model)
        {
            var id = Guid.NewGuid();
            using (var db = new SqlConnection(_connString))
            {
                var result = db.Execute($"INSERT INTO Devices(Id, CreatedOn, Name, RoomId, Status, DeviceType) VALUES (@Id, @CreatedOn, @Name, @RoomId, @Status, @DeviceType)", new { Id = id, CreatedOn = DateTime.UtcNow, Name = model.Name, RoomId = model.RoomId, Status = model.Status, DeviceType = model.DeviceType });
                if (result > 0)
                {
                    return GetDevice(id);
                }
                return null;
            }
        }

        public void DeleteDevice(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {
                db.Execute($"DELETE FROM Devices WHERE id = '{ id }'");
            }
        }

        public IEnumerable<Device> GetAllDevices(Guid roomId)
        {
            using (var db = new SqlConnection(_connString))
            {
                var devices = db.Query<Device>($"SELECT * FROM Devices WHERE RoomId = '{ roomId }'");
                return devices;
            }
        }

        public Device GetDevice(Guid id)
        {
            using (var db = new SqlConnection(_connString))
            {   // this is return IEnumerable
                var devices = db.Query<Device>($"SELECT * FROM Devices WHERE Id = '{ id }'");
                return devices.FirstOrDefault();
            }
        }

        public Device UpdateDevice(UpdateDeviceViewModel model, Guid id)
        {
            using (var db = new SqlConnection(_connString))
            { // If Name is null, then set Name(database), else set @Name(input value from user)
                var result = db.Execute(@$"
                            UPDATE Devices 
                            SET 
                                Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END, 
                                RoomId = CASE WHEN @RoomId IS NULL THEN RoomId ELSE @RoomId END, 
                                STATUS = CASE WHEN @STATUS IS NULL THEN STATUS ELSE @STATUS END, 
                                DEVICETYPE = CASE WHEN @DEVICETYPE IS NULL THEN DEVICETYPE ELSE @DEVICETYPE END 
                                WHERE id = '{ id }'", model);
                if (result > 0 ){
                    return GetDevice(id);
                }
                return null;
            }
        }
    }
}
