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
        private IDevicesRepository _devicesRepository;
        private IRoomsRepository _roomsRepository;

        public DevicesController(IDevicesRepository devicesRepository, IRoomsRepository roomsRepository)
        {
            _devicesRepository = devicesRepository;
            _roomsRepository = roomsRepository;
        }

        [HttpGet("{id}")]
        public async Task<Device> GetDevice(Guid id)
        {
            return _devicesRepository.GetDevice(id);
        }
        [HttpGet("room/{roomId}")]
        public async Task<IEnumerable<Device>> GetAllDevices(Guid roomId)
        {
            return _devicesRepository.GetAllDevices(roomId);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> AddDevice(AddDeviceViewModel model)
        {   // This is an error handling if the room with RoomId exists
            if (model.RoomId.HasValue) // the data has RoomId? then check the below. 
            {
                var room = _roomsRepository.GetRoom(model.RoomId.Value);
                if (room == null) return new BadRequestObjectResult("RoomId doesn't exist.");
            }
            
            return _devicesRepository.AddDevice(model);
        }
        
        [HttpPut("{id}")]
        public async Task<Device> UpdateDevice(UpdateDeviceViewModel model, Guid id)
        {
            return _devicesRepository.UpdateDevice(model, id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            _devicesRepository.DeleteDevice(id);
            return Ok();
        }
    }
}
