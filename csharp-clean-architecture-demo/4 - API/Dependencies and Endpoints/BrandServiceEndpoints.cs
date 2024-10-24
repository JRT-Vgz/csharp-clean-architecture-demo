﻿using _1___Entities;
using _2___Services.BrandService;
using _2___Services.Interfaces;
using _3___Mappers.Dtos.BrandDtos;
using _3___Repositories;
using _3___Validators.RequestValidators;
using _4___API.Validators.BrandValidators;
using FluentValidation;

namespace _4___API.Endpoints
{
    public static class BrandServiceEndpoints
    {
        public static void AddBrandServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<BrandEntity>, BrandRepository>();

            services.AddValidatorsFromAssemblyContaining<BrandInsertFormValidator>();
            services.AddScoped<IRequestValidator<BrandInsertDto>, BrandInsertValidator>();
            services.AddScoped<IRequestValidator<BrandUpdateDto>, BrandUpdateValidator>();

            services.AddScoped<GetAllBrandUseCase<BrandDto>>();
            services.AddScoped<GetBrandByIdUseCase<BrandDto>>();
            services.AddScoped<AddBrandUseCase<BrandInsertDto, BrandDto>>();
            services.AddScoped<UpdateBrandUseCase<BrandUpdateDto, BrandDto>>();
            services.AddScoped<DeleteBrandUseCase<BrandDto>>();
        }

        public static void MapBrandServiceEndpoints(this WebApplication app)
        {
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
            app.MapPost("/brand", async (AddBrandUseCase<BrandInsertDto, 
                BrandDto> brandUseCase, BrandInsertDto brandInsertDto,
                IValidator<BrandInsertDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(brandInsertDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                var brandDto = await brandUseCase.ExecuteAsync(brandInsertDto);

                return Results.Created($"/brand/{brandDto.Id}", brandDto);
            })
            .WithName("addBrand")
            .WithOpenApi();

            // UPDATE BRAND
            app.MapPut("/brand/{id}", async (UpdateBrandUseCase<BrandUpdateDto, BrandDto> brandUseCase, 
                BrandUpdateDto brandUpdateDto, int id,
                IValidator<BrandUpdateDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(brandUpdateDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

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
        }
    }
}
