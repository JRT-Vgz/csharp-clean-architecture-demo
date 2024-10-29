using _1___Entities;
using _2___Services.Interfaces;
using _3___Repositories;
using _3___Validators.RequestValidators;
using FluentValidation;
using _2___Services._Interfaces;
using _3___Mappers.Dtos.SaleDtos;
using _3___Mappers.ManualMappers;
using _2___Services.SaleService;
using _3___Data.Models;
using _4___API.FormValidators.SaleFormValidators;

namespace _4___API.Dependencies_and_Endpoints
{
    public static class SaleServiceEndpoints
    {
        public static void AddSaleServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<SaleEntity>, SaleRepository>();
            services.AddScoped<IRepositorySearch<SaleModel, SaleEntity>, SaleRepository>();
            services.AddScoped<IManualMapper<SaleInsertDto, SaleEntity>, SaleInsertMapper>();

            services.AddValidatorsFromAssemblyContaining<SaleInsertFormValidator>();
            services.AddScoped<IRequestValidator<SaleInsertDto>, SaleInsertValidator>();

            services.AddScoped<AddSaleUseCase<SaleInsertDto>>();
            services.AddScoped<GetAllSaleUseCase>();
            services.AddScoped<GetSaleByIdUseCase>();
            services.AddScoped<SearchAllSaleUseCase<SaleModel>>();
        }

        public static void MapSaleServiceEndpoints(this WebApplication app)
        {
            // GET ALL SALE
            app.MapGet("/sale", async (GetAllSaleUseCase saleUseCase) =>
            {
                return await saleUseCase.ExecuteAsync();
            })
            .WithName("getAllSale")
            .WithOpenApi();

            // SEARCH ALL SALE BY TOTAL
            app.MapGet("/sale/search/{total}", async (SearchAllSaleUseCase<SaleModel> saleUseCase, decimal total) =>
            {
                return await saleUseCase.ExecuteAsync(s => s.Total >= total);
            })
            .WithName("searchAllSaleByTotal")
            .WithOpenApi();

            // GET SALE BY ID
            app.MapGet("/sale/{id}", async (GetSaleByIdUseCase saleUseCase, int id) =>
            {
                return await saleUseCase.ExecuteAsync(id);
            })
            .WithName("getSaleById")
            .WithOpenApi();

            // ADD SALE
            app.MapPost("/sale", async (AddSaleUseCase<SaleInsertDto> saleUseCase, SaleInsertDto saleInsertDto,
                IValidator<SaleInsertDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(saleInsertDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                await saleUseCase.ExecuteAsync(saleInsertDto);

                return Results.Created();

            })
            .WithName("addSale")
            .WithOpenApi();
        }
    }
}
