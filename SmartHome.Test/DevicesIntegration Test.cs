using Microsoft.AspNetCore.Mvc.Testing;
using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SmartHome.Test
{
    public class DevicesIntegration_Test : IClassFixture<WebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _factory;

        public DevicesIntegration_Test(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task<Device> AddDevice()
        {   // arrange
            var client = _factory.CreateClient();
            var deviceViewModel = new AddDeviceViewModel
            {
                Name = "Light bulb",
                RoomId = null,
                Status = "new",
                DeviceType = DeviceType.Light
            };
            // act
            var response = await client.PostAsJsonAsync("api/devices", deviceViewModel);
            Assert.True(response.IsSuccessStatusCode); // checks if the response is success
            var data = JsonSerializer.Deserialize<Device>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // deserialize response 
            // assert
            Assert.Equal(deviceViewModel.Name, data.Name); // checks if the expected result and the actual result are same
            return data; // then you can use AddRoom() method below
        }
        [Fact]
        public async Task GetDevice() // get a device
        {
            var client = _factory.CreateClient();
            var device = await AddDevice();
            var response = await client.GetAsync($"api/devices/{device.Id}");
            Assert.True(response.IsSuccessStatusCode);
            var data = JsonSerializer.Deserialize<Device>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.NotNull(data); // checks if the response exists
            Assert.Equal(device.Name, data.Name); // (expected result, actual result)
        }

        [Fact]
        public async Task GetAllDevices() // get all devices
        {
            var client = _factory.CreateClient();
            await AddDevice();
            var response = await client.GetAsync("api/devices");
            Assert.True(response.IsSuccessStatusCode);
            var data = JsonSerializer.Deserialize<List<Device>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(200, (int)response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(data.Count > 0); // this is a list
        }
        [Fact]
        public async Task GetAllDevicesFromRoomId() // get all devices from a room 
        {   // arrange
            var client = _factory.CreateClient();
            var roomTest = new RoomsIntegrationTest(_factory); // add a room using RoomIntegration test to get a roomId
            var room = await roomTest.AddRoom();
            var deviceViewModel = new AddDeviceViewModel
            {
             Name = "Sensor",
             RoomId = room.Id,
             Status = "old",
             DeviceType = DeviceType.MotionSensor
             };
            await client.PostAsJsonAsync("api/devices", deviceViewModel); // add a device with the roomId
            // act
            var response = await client.GetAsync($"api/devices?roomId={deviceViewModel.RoomId}"); //
            Assert.True(response.IsSuccessStatusCode); // checks if the response is success
            var data = JsonSerializer.Deserialize<List<Device>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // deserialize response 
            // assert
            Assert.Equal(200, (int)response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(data.Count > 0); // this is a list
        }
        [Fact]
        public async Task DeleteDevice() 
        {
            var client = _factory.CreateClient();
            var device = await AddDevice();
            var response = await client.DeleteAsync($"api/devices/{device.Id}");
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async Task UpdateDevice() 
        {   // arrange
            var client = _factory.CreateClient();
            var device = await AddDevice();
            var deviceViewModel = new UpdateDeviceViewModel
            {
                Name = "Button",
                RoomId = null,
                Status = null,
                DeviceType = DeviceType.Button
            };
            // act
            var response = await client.PutAsJsonAsync($"api/devices/{device.Id}", deviceViewModel);
            Assert.True(response.IsSuccessStatusCode);
            var data = JsonSerializer.Deserialize<Device>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.NotNull(data); // checks if the response exists
            Assert.Equal(deviceViewModel.Name, data.Name); // (expected result, actual result)
        }
    }
}
