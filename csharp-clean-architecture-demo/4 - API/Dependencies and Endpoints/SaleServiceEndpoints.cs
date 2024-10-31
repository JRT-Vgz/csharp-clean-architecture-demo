using _1___Entities;
using _2___Services.Interfaces;
using _3___Repositories;
using FluentValidation;
using _2___Services._Interfaces;
using _3___Mappers.Dtos.SaleDtos;
using _3___Mappers.ManualMappers;
using _3___Data.Models;
using _4___API.FormValidators.SaleFormValidators;
using _2___Services.Exceptions;
using _2___Services.Services.SaleService;
using _3___Validators.EntityValidators;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;

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
            services.AddScoped<IEntityValidator<SaleEntity>, SaleEntityValidator>();

            services.AddScoped<AddSaleUseCase<SaleInsertDto, SaleDto>>();
            services.AddScoped<GetAllSaleUseCase<SaleDto>>();
            services.AddScoped<GetSaleByIdUseCase<SaleDto>>();
            services.AddScoped<SearchAllSaleUseCase<SaleModel, SaleDto>>();
            services.AddScoped<DeleteSaleUseCase<SaleDto>>();
            services.AddScoped<UpdateSaleUseCase>();
        }

        public static void MapSaleServiceEndpoints(this WebApplication app)
        {
            // GET ALL SALE
            app.MapGet("/sale", async (GetAllSaleUseCase<SaleDto> saleUseCase) =>
            {               
                return await saleUseCase.ExecuteAsync();
            })
            .WithName("getAllSale")
            .WithOpenApi();

            // SEARCH ALL SALE BY TOTAL
            app.MapGet("/sale/search/{total}", async (SearchAllSaleUseCase<SaleModel, SaleDto> saleUseCase, decimal total) =>
            {
                return await saleUseCase.ExecuteAsync(s => s.Total >= total);
            })
            .WithName("searchAllSaleByTotal")
            .WithOpenApi();

            // GET SALE BY ID
            app.MapGet("/sale/{id}", async (GetSaleByIdUseCase<SaleDto> saleUseCase, int id) =>
            {
                return await saleUseCase.ExecuteAsync(id);
            })
            .WithName("getSaleById")
            .WithOpenApi();

            // ADD SALE
            app.MapPost("/sale", async (AddSaleUseCase<SaleInsertDto, SaleDto> saleUseCase, SaleInsertDto saleInsertDto,
                IValidator<SaleInsertDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(saleInsertDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                var saleDto = await saleUseCase.ExecuteAsync(saleInsertDto);

                return Results.Created($"/sale/{saleDto.Id}", saleDto);

            })
            .WithName("addSale")
            .WithOpenApi();

            // UPDATE SALE
            app.MapPut("/sale/{id}", (UpdateSaleUseCase saleUseCase, int id) =>
            {
                saleUseCase.Execute();
            })
            .WithName("updateSale")
            .WithOpenApi();

            // DELETE SALE
            app.MapDelete("/sale/{id}", async (DeleteSaleUseCase<SaleDto> saleUseCase, int id) =>
            {
                return await saleUseCase.ExecuteAsync(id);
            })
            .WithName("deleteSale")
            .WithOpenApi();
        }
    }
}
