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
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private IRoomsRepository _roomsRepository;

        public RoomsController(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return _roomsRepository.GetAllRooms();
        }
        [HttpGet]
        public async Task<Room> GetRoom(Guid id)
        {
            return _roomsRepository.GetRoom(id);
        }
        [HttpPut("{id}")]
        public async Task<Room> UpdateRoom(Guid id, UpdateRoomViewModel model)
        {
            return _roomsRepository.UpdateRoom(id, model);
        }
        [HttpDelete("{id}")]
        public async Task<Room> AddRoom(AddRoomViewModel model)
        {
           return _roomsRepository.AddRoom(model);
        }
        public async Task<ActionResult> DeleteRoom(Guid id)
        {
            _roomsRepository.DeleteRoom(id);
            return Ok();
        }
    }
}
