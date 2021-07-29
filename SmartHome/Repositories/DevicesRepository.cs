using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Repositories
{
    public interface IDevicesRepository
    {
        Device AddDevice(AddDeviceViewModel model);
        Device GetDevice(Guid id);
        IEnumerable<Device> GetAllDevices();
        Device UpdateDevoce(UpdateDeviceViewModel model, Guid id);
        void DeleteDevice(Guid id);

    }
    public class DevicesRepository : IDevicesRepository
    {
        public Device AddDevice(AddDeviceViewModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteDevice(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Device> GetAllDevices()
        {
            throw new NotImplementedException();
        }

        public Device GetDevice(Guid id)
        {
            throw new NotImplementedException();
        }

        public Device UpdateDevoce(UpdateDeviceViewModel model, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
