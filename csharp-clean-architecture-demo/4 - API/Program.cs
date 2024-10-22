using _1___Entities;
using _2___Services.BeerService;
using _2___Services.Interfaces;
using _3___Data;
using _3___Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DEPENDENCIAS
builder.Services.AddScoped<IRepository<BeerEntity>, BeerRepository>();
builder.Services.AddScoped<GetAllBeerUseCase>();



// ENTITY FRAMEWORK
builder.Services.AddDbContext<BreweryContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("BreweryConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/beer", async (GetAllBeerUseCase beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("getAllBeer")
.WithOpenApi();

app.Run();

