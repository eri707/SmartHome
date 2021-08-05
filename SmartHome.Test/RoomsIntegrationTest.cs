using Microsoft.AspNetCore.Mvc.Testing; //This packages includes a WebApplicationFactory<TEntryPoint> class which is used to create the API in memory. 
using SmartHome.Model;
using SmartHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit; // .NET testing framework

namespace SmartHome.Test
{ // In the test class, we inject the factory into the constructor to create a HttpClient which is used in the tests to make HTTP requests.
    public class RoomsIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RoomsIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task<Room> AddRoom() // always you need to add a room first
        {   // arrange
            var client = _factory.CreateClient();
            var roomViewModel = new AddRoomViewModel // create a new AddRoomViewModel class
            {
                Name = "Living room"
            };
            // act
            var response = await client.PostAsJsonAsync("api/rooms", roomViewModel);
            Assert.True(response.IsSuccessStatusCode); // checks if the response is success
            var data = JsonSerializer.Deserialize<Room>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }); // deserialize response 
            // assert
            Assert.Equal(roomViewModel.Name, data.Name); // checks if the expected result and the actual result are same
            return data; // then you can use AddRoom() method below
        }

        [Fact] // purpose is to test the happy path. what you're expecting the user to do
        public async Task GetAllRooms() // not Task<> since you don't need to return type
        {
            //arrange
            var client = _factory.CreateClient();
            var room = await AddRoom(); // add a room
            //act
            var response = await client.GetAsync("api/rooms");
            var data = JsonSerializer.Deserialize<List<Room>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.Equal(200, (int)response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(data.Count > 0); // this is a list 

            //clean up(delete the added room)
            var deleted = await client.DeleteAsync($"api/rooms/{room.Id}");
            // assert
            Assert.True(deleted.IsSuccessStatusCode);
        }
        [Fact]
        public async Task GetRoom() 
        {
            //arrange
            var client = _factory.CreateClient();
            var room = await AddRoom();
            //act
            var response = await client.GetAsync($"api/rooms/{room.Id}");
            Assert.True(response.IsSuccessStatusCode);
            var data = JsonSerializer.Deserialize<Room>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.NotNull(data); // checks if the response exists
            Assert.Equal(room.Name, data.Name); // (expected result, actual result)
        }
        [Fact]
        public async Task DeleteRoom()
        {
            //arrange
            var client = _factory.CreateClient();
            var room = await AddRoom();
            //act
            var response = await client.DeleteAsync($"api/rooms/{room.Id}");
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async Task UpdateRoom()
        {
            //arrange
            var client = _factory.CreateClient();
            var room = await AddRoom(); // add a room
            var roomViewModel = new UpdateRoomViewModel
            {
                Name = "Family room"
            };
            //act
            var response = await client.PutAsJsonAsync($"api/rooms/{room.Id}", roomViewModel);
            Assert.True(response.IsSuccessStatusCode);
            var data = JsonSerializer.Deserialize<Room>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.NotNull(data); // checks if the response exists
            Assert.Equal(roomViewModel.Name, data.Name); // (expected result, actual result)
        }
    }
}
