
using _3___Mappers.Dtos.BeerDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace BeerServiceEndpoint.Tests
{
    public class AddBeerEndpointTest
    {
        [Fact(Skip = "Crea el objeto cada vez, por lo que solo se puede testear la primera vez.")]
        public async Task CreateBeer_OK()
        {
            var beerInsertDto = new BeerInsertDto { Name = "Beer", IdBrand = 1, Alcohol = 10 };

            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer";
            var result = await client.PostAsJsonAsync(endpoint, beerInsertDto);

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        //[Fact(Skip = "Solo se puede testear si ya existe el objeto.")]
        [Fact]
        public async Task CreateBeer_Conflict_BeerAlreadyExists()
        {
            var beerInsertDto = new BeerInsertDto { Name = "Beer", IdBrand = 1, Alcohol = 10 };

            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer";
            var result = await client.PostAsJsonAsync(endpoint, beerInsertDto);

            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        }

        [Fact]
        public async Task CreateBeer_BadRequest_InvalidForm()
        {
            var beerInsertDto = new BeerInsertDto { Name = "", IdBrand = 1, Alcohol = 10 };

            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var endpoint = "/beer";
            var result = await client.PostAsJsonAsync(endpoint, beerInsertDto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
