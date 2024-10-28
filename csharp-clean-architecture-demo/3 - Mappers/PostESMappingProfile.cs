
using _1___Entities;
using _3____Adapters.Dtos;
using AutoMapper;

namespace _3___Mappers
{
    public class PostESMappingProfile : Profile
    {
        public PostESMappingProfile() 
        {
            CreateMap<PostESDto, PostEntity>();
        }

    }
}
