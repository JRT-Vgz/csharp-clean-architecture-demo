using _3___Data;
using _3___Mappers.AutoMappers;
using _4___API.Dependencies_and_Endpoints;
using _4___API.Endpoints;
using _4___API.Middlewares;
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
builder.Services.AddAutoMapper(typeof(MappingProfile));

// BEER SERVICE DEPENDENCIES
builder.Services.AddBeerServiceDependencies();

// BRAND SERVICE DEPENDENCIES
builder.Services.AddBrandServiceDependencies();

// POST SERVICE DEPENDENCIES
builder.Services.AddPostServiceDependencies(builder.Configuration);

// SALE SERVICE DEPENDENCIES
builder.Services.AddSaleServiceDependencies();

// ---------------------------------------------------------------------------------------
// ------------------------------  BUILD AND CONFIGURATION  ------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// -------------------------------------  ENDPOINTS  -------------------------------------
// MIDDLEWARES
app.UseMiddleware<RequestValidationExceptionMiddleware>();
app.UseMiddleware<NotFoundExceptionMiddleware>();
app.UseMiddleware<HttpRequestExceptionMiddleware>();

// BEER SERVICE ENDPOINTS
app.MapBeerServiceEndpoints();

// BRAND SERVICE ENDPOINTS
app.MapBrandServiceEndpoints();

// POST EXTERNAL SERVICE ENDPOINTS
app.MapPostServiceEndpoints();

// SALE SERVICE ENDPOINTS
app.MapSaleServiceEndpoints();


// ---------------------------------------------------------------------------------------



app.Run();

