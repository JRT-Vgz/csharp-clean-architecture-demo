﻿using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using _3___Presenters;
using _3___Presenters.ViewModels;
using _3___Repositories;
using _3___Validators.EntityValidators;
using _4___API.FormValidators.BeerValidators;
using FluentValidation;

namespace _4___API.Endpoints
{
    public static class BeerServiceEndpoints
    {

        public static void AddBeerServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<BeerEntity>, BeerRepository>();
            services.AddScoped<IPresenter<BeerEntity, BeerDetailViewModel>, BeerDetailPresenter>();

            services.AddValidatorsFromAssemblyContaining<BeerInsertFormValidator>();
            services.AddScoped<IEntityValidator<BeerEntity>, BeerEntityValidator>();

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
            app.MapPost("/beer", async (AddBeerUseCase<BeerInsertDto, BeerDto> beerUseCase, 
                BeerInsertDto beerInsertDto,
                IValidator<BeerInsertDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(beerInsertDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                var beerDto = await beerUseCase.ExecuteAsync(beerInsertDto);

                return Results.Created($"/beer/{beerDto.Id}", beerDto);
            })
            .WithName("addBeer")
            .WithOpenApi();

            // UPDATE BEER
            app.MapPut("/beer/{id}", async (UpdateBeerUseCase<BeerUpdateDto, BeerDto> beerUseCase, 
                BeerUpdateDto beerUpdateDto, int id,
                IValidator<BeerUpdateDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(beerUpdateDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                if (beerUpdateDto.Id != id) { return Results.BadRequest($"El ID en el cuerpo de la solicitud ({beerUpdateDto.Id}) no coincide con el ID en la ruta ({id})."); }

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
