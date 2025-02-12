﻿using _1___Entities;
using _2___Services._Interfaces;
using FluentValidation;
using _3____Adapters.Interfaces;
using _4___ExternalServices;
using _3____Adapters;
using _2___Services.Services.PostService;
using _3___Mappers.Dtos.AdaptersDtos;

namespace _4___API.Dependencies_and_Endpoints
{
    public static class PostServiceEndpoints
    {
        public static void AddPostServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddScoped<IExternalServiceAdapter<PostEntity>, PostESAdapter>();
            services.AddScoped<IExternalService<PostESDto>, PostExternalService>();

            services.AddScoped<GetAllPostUseCase>();
            services.AddScoped<GetPostByIdUseCase>();

            services.AddHttpClient<IExternalService<PostESDto>, PostExternalService>(c =>
            {
                c.BaseAddress = new Uri(configuration["External Service URLs:PostESUrl"]);
            });
        }

        public static void MapPostServiceEndpoints(this WebApplication app)
        {
            // GET ALL POSTS
            app.MapGet("/post", async (GetAllPostUseCase postUseCase) =>
            {
                return await postUseCase.ExecuteAsync();
            })
            .WithName("getAllPost")
            .WithOpenApi();

            // GET POST BY ID
            app.MapGet("/post/{id}", async (GetPostByIdUseCase postUseCase, int id) =>
            {
                var postEntity = await postUseCase.ExecuteAsync(id);
                return postEntity;
            })
            .WithName("getPostById")
            .WithOpenApi();
        }
    }
}
