using _1___Entities;
using _2___Services.Interfaces;
using _2___Services.Services.BeerService;
using _3___Data;
using _3___Mappers.AutoMappers;
using _3___Mappers.Dtos.BeerDtos;
using _3___Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var container = new ServiceCollection()
    .AddDbContext<BreweryContext>(options =>
    {
        options.UseMySQL(configuration.GetConnectionString("BreweryConnection"));
    })
    .AddScoped<IRepository<BeerEntity>, BeerRepository>()
    .AddAutoMapper(typeof(MappingProfile))
    .AddScoped<GetAllBeerUseCase<BeerDto>>()
    .BuildServiceProvider();

var beerUseCase = container.GetService<GetAllBeerUseCase<BeerDto>>();
var beers = await beerUseCase.ExecuteAsync();

// Mostrar los resultados
foreach (var beer in beers)
{
    Console.WriteLine($"Id: {beer.Id}, Name: {beer.Name}, Brand: {beer.BrandName}, Alcohol: {beer.Alcohol}.");
}