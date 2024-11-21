
using _1___Entities;
using _3___Mappers.Dtos.BeerDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;

namespace BeerServiceEndpoint.Tests
{
    public class GetAllBeerEndpointTest
    {
        [Fact]
        public async Task TestEndpoint_OK()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer";
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var beers = JsonConvert.DeserializeObject<List<BeerDto>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(beers);
            Assert.IsType<List<BeerDto>>(beers);
        }
    }
}
