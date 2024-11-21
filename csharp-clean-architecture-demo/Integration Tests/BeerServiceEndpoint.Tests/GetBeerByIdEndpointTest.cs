
using _3___Mappers.Dtos.BeerDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace BeerServiceEndpoint.Tests
{
    public class GetBeerByIdEndpointTest
    {
        [Fact]
        public async Task TestEndpoint_OK()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer/1";
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var beer = JsonSerializer.Deserialize<BeerDto>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<BeerDto>(beer);
        }

        [Fact]
        public async Task TestEndpoint_NotFound()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer/1000";
            var response = await client.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var beer = JsonSerializer.Deserialize<BeerDto>(content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
