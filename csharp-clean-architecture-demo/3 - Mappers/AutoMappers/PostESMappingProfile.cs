using _1___Entities;
using _3___Mappers.Dtos.AdaptersDtos;
using AutoMapper;

namespace _3___Mappers.AutoMappers
{
    public class PostESMappingProfile : Profile
    {
        public PostESMappingProfile()
        {
            CreateMap<PostESDto, PostEntity>();
        }

    }
}
