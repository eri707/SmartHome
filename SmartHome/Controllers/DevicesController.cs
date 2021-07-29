using Microsoft.AspNetCore.Mvc;
using SmartHome.Model;
using SmartHome.Repositories;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private IDevicesRepository _deviceRepository;

        public DevicesController(IDevicesRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet("{id}")]
        public async Task<Device> GetDevice(Guid id)
        {
            return _deviceRepository.GetDevice(id);
        }
        [HttpGet]
        public async Task<IEnumerable<Device>> GetAllDevices()
        {
            return _deviceRepository.GetAllDevices();
        }

        [HttpPost]
        public async Task<Device> AddDevice(AddDeviceViewModel model)
        {
            return _deviceRepository.AddDevice(model);
        }
        
        [HttpPut("{id}")]
        public async Task<Device> UpdateDevice(UpdateDeviceViewModel model, Guid id)
        {
            return _deviceRepository.UpdateDevoce(model, id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            _deviceRepository.DeleteDevice(id);
            return Ok();
        }






       
    }
}
