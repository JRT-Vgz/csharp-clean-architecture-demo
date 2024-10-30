using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Interfaces;
using _2___Services.Services.BrandService;
using _2___Services.Services.ConceptService;
using _3___Data.Models;
using _3___Mappers.Dtos.BrandDtos;
using _3___Mappers.Dtos.SaleDtos;
using _3___Repositories;
using AutoMapper;

namespace _4___API.Dependencies_and_Endpoints
{
    public static class ConceptServiceEndpoints
    {
        public static void AddConceptServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ConceptEntity>, ConceptRepository>();
            services.AddScoped<IRepository<SaleEntity>, SaleRepository>();
            services.AddScoped<IRepositorySearch<ConceptModel, ConceptEntity>, ConceptRepository>();

            services.AddScoped<GetAllConceptUseCase<ConceptDto>>();
            services.AddScoped<GetConceptByIdUseCase<ConceptDto>>();
            services.AddScoped<SearchAllConceptUseCase<ConceptModel, ConceptDto>>();
            services.AddScoped<DeleteConceptUseCase<ConceptDto>>();
        }

        public static void MapConceptServiceEndpoints(this WebApplication app)
        {
            // GET ALL CONCEPTS
            app.MapGet("/concept", async (GetAllConceptUseCase<ConceptDto> conceptUseCase) =>
            {
                return await conceptUseCase.ExecuteAsync();
            })
            .WithName("getAllConcept")
            .WithOpenApi();

            // SEARCH ALL CONCEPTS BY PRICE
            app.MapGet("/concept/search/{price}", async (SearchAllConceptUseCase<ConceptModel, ConceptDto> conceptUseCase, decimal price) =>
            {
                return await conceptUseCase.ExecuteAsync(s => (s.UnitPrice * s.Quantity) >= price);
            })
            .WithName("searchAllConceptByTotal")
            .WithOpenApi();

            // GET CONCEPT BY ID
            app.MapGet("/concept/{id}", async (GetConceptByIdUseCase<ConceptDto> conceptUseCase, int id) =>
            {
                return await conceptUseCase.ExecuteAsync(id);
            })
            .WithName("getConceptById")
            .WithOpenApi();

            // DELETE CONCEPT
            app.MapDelete("/concept/{id}", async (DeleteConceptUseCase<ConceptDto> conceptUseCase, int id) =>
            {
                return await conceptUseCase.ExecuteAsync(id);
            })
            .WithName("deleteConcept")
            .WithOpenApi();

        }
    }
}
