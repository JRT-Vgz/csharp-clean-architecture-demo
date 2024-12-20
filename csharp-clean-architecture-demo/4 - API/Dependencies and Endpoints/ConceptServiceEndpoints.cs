﻿using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Services.ConceptService;
using _3___Data.Models;
using _3___Mappers.Dtos.SaleDtos;
using _3___Repositories;
using _3___Validators.EntityValidators;
using _4___API.FormValidators.ConceptFormValidators;
using FluentValidation;

namespace _4___API.Dependencies_and_Endpoints
{
    public static class ConceptServiceEndpoints
    {
        public static void AddConceptServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ConceptEntity>, ConceptRepository>();
            services.AddScoped<IRepository<SaleEntity>, SaleRepository>();
            services.AddScoped<IRepositorySearch<ConceptModel, ConceptEntity>, ConceptRepository>();

            services.AddValidatorsFromAssemblyContaining<ConceptUpdateFormValidator>();
            services.AddScoped<IEntityValidator<ConceptEntity>, ConceptEntityValidator>();

            services.AddScoped<GetAllConceptUseCase<ConceptDto>>();
            services.AddScoped<GetConceptByIdUseCase<ConceptDto>>();
            services.AddScoped<SearchAllConceptUseCase<ConceptModel, ConceptDto>>();
            services.AddScoped<DeleteConceptUseCase<ConceptDto>>();
            services.AddScoped<UpdateConceptUseCase<ConceptUpdateDto, ConceptDto>>();
            services.AddScoped<AddConceptToIdSaleUseCase<ConceptInsertToIdSaleDto, SaleDto>>();
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

            // INSERT CONCEPT TO ID SALE
            app.MapPost("concept/addto/{idSale}", async (AddConceptToIdSaleUseCase<ConceptInsertToIdSaleDto, SaleDto> conceptUseCase,
                ConceptInsertToIdSaleDto conceptInsertToIdSaleDto, int idSale,
                IValidator<ConceptInsertToIdSaleDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(conceptInsertToIdSaleDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                if (conceptInsertToIdSaleDto.IdSale != idSale) { return Results.BadRequest($"El ID en el cuerpo de la solicitud ({conceptInsertToIdSaleDto.IdSale}) no coincide con el ID en la ruta ({idSale})."); }

                var conceptDto = await conceptUseCase.ExecuteAsync(conceptInsertToIdSaleDto, idSale);

                return Results.Ok(conceptDto);
            })
            .WithName("addConceptToIdSale")
            .WithOpenApi();

            // UPDATE CONCEPT
            app.MapPut("/concept/{id}", async (UpdateConceptUseCase<ConceptUpdateDto, ConceptDto> conceptUseCase, 
                ConceptUpdateDto conceptUpdateDto, int id,
                IValidator<ConceptUpdateDto> formValidator) =>
            {
                var formValidationResult = formValidator.Validate(conceptUpdateDto);
                if (!formValidationResult.IsValid) { return Results.ValidationProblem(formValidationResult.ToDictionary()); }

                if (conceptUpdateDto.Id != id) { return Results.BadRequest($"El ID en el cuerpo de la solicitud ({conceptUpdateDto.Id}) no coincide con el ID en la ruta ({id})."); }

                var conceptDto = await conceptUseCase.ExecuteAsync(conceptUpdateDto, id);

                return Results.Ok(conceptDto);
            })
            .WithName("updateConcept")
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
