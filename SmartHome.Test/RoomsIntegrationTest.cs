using Microsoft.AspNetCore.Mvc.Testing;
using SmartHome.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SmartHome.Test
{
    public class RoomsIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RoomsIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRooms()
        {
            //arrange
            var client = _factory.CreateClient();
            //act
            var response = await client.GetAsync("api/rooms");
            var data = JsonSerializer.Deserialize<List<Room>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //assert
            Assert.Equal(200, (int)response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(data.Count > 0);
        }
    }
}
