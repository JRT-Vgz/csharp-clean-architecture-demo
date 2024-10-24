using _1___Entities;
using _2___Services.BeerService;
using _2___Services.BrandService;
using _2___Services.Interfaces;
using _3___Data;
using _3___Mappers;
using _3___Mappers.Dtos.BeerDtos;
using _3___Mappers.Dtos.BrandDtos;
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

// BEER SERVICE DEPENDENCIES
builder.Services.AddScoped<IRepository<BeerEntity>, BeerRepository>();
builder.Services.AddScoped<IPresenter<BeerEntity, BeerDetailViewModel>, BeerDetailPresenter>();

builder.Services.AddScoped<GetAllBeerUseCase<BeerDto>>();
builder.Services.AddScoped<GetBeerByIdUseCase<BeerDto>>();
builder.Services.AddScoped<AddBeerUseCase<BeerInsertDto, BeerDto>>();
builder.Services.AddScoped<UpdateBeerUseCase<BeerUpdateDto, BeerDto>>();
builder.Services.AddScoped<DeleteBeerUseCase<BeerDto>>();
builder.Services.AddScoped<GetAllBeerDetailUseCase<BeerDetailViewModel>>();
builder.Services.AddScoped<GetBeerDetailByIdUseCase<BeerDetailViewModel>>();

// BRAND SERVICE DEPENDENCIES
builder.Services.AddScoped<IRepository<BrandEntity>, BrandRepository>();

builder.Services.AddScoped<GetAllBrandUseCase<BrandDto>>();
builder.Services.AddScoped<GetBrandByIdUseCase<BrandDto>>();
builder.Services.AddScoped<AddBrandUseCase<BrandInsertDto, BrandDto>>();
builder.Services.AddScoped<UpdateBrandUseCase<BrandUpdateDto, BrandDto>>();
builder.Services.AddScoped<DeleteBrandUseCase<BrandDto>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// -------------------------------------  ENDPOINTS  -------------------------------------
// ------ BEER SERVICE ENDPOINTS ------
// GET ALL BEER
app.MapGet("/beer", async (GetAllBeerUseCase<BeerDto> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("getAllBeer")
.WithOpenApi();

// GET BEER BY ID
app.MapGet("/beer/{id}", async (GetBeerByIdUseCase<BeerDto> beerUseCase, int id) =>
{
    return await beerUseCase.ExecuteAsync(id);
})
.WithName("getBeerById")
.WithOpenApi();

// ADD BEER
app.MapPost("/beer", async (AddBeerUseCase<BeerInsertDto, BeerDto> beerUseCase, BeerInsertDto beerInsertDto) =>
{
    var beerDto = await beerUseCase.ExecuteAsync(beerInsertDto);

    return Results.Created($"/beer/{beerDto.Id}", beerDto);
})
.WithName("addBeer")
.WithOpenApi();

// UPDATE BEER
app.MapPut("/beer/{id}", async (UpdateBeerUseCase<BeerUpdateDto, BeerDto> beerUseCase, BeerUpdateDto beerUpdateDto, int id) =>
{
    var beerDto = await beerUseCase.ExecuteAsync(beerUpdateDto, id);

    return Results.Ok(beerDto);
})
.WithName("updateBeer")
.WithOpenApi();

// DELETE BEER
app.MapDelete("/beer/{id}", async (DeleteBeerUseCase<BeerDto> beerUseCase, int id) =>
{
    return await beerUseCase.ExecuteAsync(id);
})
.WithName("deleteBeer")
.WithOpenApi();

// GET ALL BEER DETAIL
app.MapGet("/beerDetail", async (GetAllBeerDetailUseCase<BeerDetailViewModel> beerUseCase) =>
{
    return await beerUseCase.ExecuteAsync();
})
.WithName("getAllBeerDetail")
.WithOpenApi();

// GET BEER BY ID DETAIL
app.MapGet("/beerDetail/{id}", async (GetBeerDetailByIdUseCase<BeerDetailViewModel> beerUseCase, int id) =>
{
    return await beerUseCase.ExecuteAsync(id);
})
.WithName("getBeerDetailById")
.WithOpenApi();




// ---------------------------------------------------------------------------------------
// ------ BRAND SERVICE ENDPOINTS ------
// GET ALL BRAND
app.MapGet("/brand", async (GetAllBrandUseCase<BrandDto> brandUseCase) =>
{
    return await brandUseCase.ExecuteAsync();
})
.WithName("getAllBrand")
.WithOpenApi();

// GET BRAND BY ID
app.MapGet("/brand/{id}", async (GetBrandByIdUseCase<BrandDto> brandUseCase, int id) =>
{
    return await brandUseCase.ExecuteAsync(id);
})
.WithName("getBrandById")
.WithOpenApi();

// ADD BRAND
app.MapPost("/brand", async (AddBrandUseCase<BrandInsertDto, BrandDto> brandUseCase, BrandInsertDto brandInsertDto) =>
{
    var brandDto = await brandUseCase.ExecuteAsync(brandInsertDto);

    return Results.Created($"/brand/{brandDto.Id}", brandDto);
})
.WithName("addBrand")
.WithOpenApi();

// UPDATE BRAND
app.MapPut("/brand/{id}", async (UpdateBrandUseCase<BrandUpdateDto, BrandDto> brandUseCase, BrandUpdateDto brandUpdateDto, int id) =>
{
    var brandDto = await brandUseCase.ExecuteAsync(brandUpdateDto, id);

    return Results.Ok(brandDto);
})
.WithName("updateBrand")
.WithOpenApi();

// DELETE BRAND
app.MapDelete("/brand/{id}", async (DeleteBrandUseCase<BrandDto> brandUseCase, int id) =>
{
    return await brandUseCase.ExecuteAsync(id);
})
.WithName("deleteBrand")
.WithOpenApi();




app.Run();

