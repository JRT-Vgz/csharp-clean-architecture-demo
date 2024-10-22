using _1___Entities;
using _2___Services.BeerService;
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers;
using _3___Mappers.Dtos.BeerDtos;
using _3___Presenters;
using _3___Presenters.ViewModels;
using _3___Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// -------------------------------------  DEPENDENCIES  -------------------------------------
// ENTITY FRAMEWORK
builder.Services.AddDbContext<BreweryContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("BreweryConnection"));
});

// AUTOMAPPERS
builder.Services.AddAutoMapper(typeof(BeerMappingProfile));

// BEER ENTITY DEPENDENCIES
builder.Services.AddScoped<IRepository<BeerEntity>, BeerRepository>();
builder.Services.AddScoped<IPresenter<BeerEntity, BeerViewModel>, BeerPresenter>();

builder.Services.AddScoped<GetAllBeerUseCase<BeerViewModel>>();
builder.Services.AddScoped<GetBeerByIdUseCase<BeerViewModel>>();
builder.Services.AddScoped<AddBeerUseCase<BeerInsertDto, BeerDto>>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// -------------------------------------  ENDPOINTS  -------------------------------------
// ------ BEER ENTITY ENDPOINTS ------
// GET ALL BEER
app.MapGet("/beer", async (GetAllBeerUseCase<BeerViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("getAllBeer")
.WithOpenApi();

// GET BEER BY ID
app.MapGet("/beer/{id}", async (GetBeerByIdUseCase<BeerViewModel> beerUseCase, int id) =>
{
    return await beerUseCase.ExecuteAsync(id);
})
.WithName("getBeerById")
.WithOpenApi();

// ADD BEER
app.MapPost("/beer", async (AddBeerUseCase<BeerInsertDto, BeerDto> beerUseCase, BeerInsertDto beerInsertDto) =>
{
    return await beerUseCase.ExecuteAsync(beerInsertDto);
})
.WithName("addBeer")
.WithOpenApi();


app.Run();

