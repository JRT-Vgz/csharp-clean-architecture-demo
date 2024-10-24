using _1___Entities;
using _2___Services.BeerService;
using _2___Services.Interfaces;
using _3___Mappers.Dtos.BeerDtos;
using _3___Presenters;
using _3___Presenters.ViewModels;
using _3___Repositories;

namespace _4___API.Endpoints
{
    public static class BeerServiceEndpoints
    {

        public static void AddBeerServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<BeerEntity>, BeerRepository>();
            services.AddScoped<IPresenter<BeerEntity, BeerDetailViewModel>, BeerDetailPresenter>();

            services.AddScoped<GetAllBeerUseCase<BeerDto>>();
            services.AddScoped<GetBeerByIdUseCase<BeerDto>>();
            services.AddScoped<AddBeerUseCase<BeerInsertDto, BeerDto>>();
            services.AddScoped<UpdateBeerUseCase<BeerUpdateDto, BeerDto>>();
            services.AddScoped<DeleteBeerUseCase<BeerDto>>();
            services.AddScoped<GetAllBeerDetailUseCase<BeerDetailViewModel>>();
            services.AddScoped<GetBeerDetailByIdUseCase<BeerDetailViewModel>>();
        }

        public static void MapBeerServiceEndpoints(this WebApplication app)
        {
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
        }
    }
}
