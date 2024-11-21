using _1___Entities;
using _3___Data.Models;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;

namespace _3___Mappers.AutoMappers
{
    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile()
        {
            CreateMap<BeerInsertDto, BeerEntity>()
                .ForMember(dest => dest.Id, map => map.Ignore())
                .ForMember(dest => dest.BrandName, map => map.Ignore());

            CreateMap<BeerUpdateDto, BeerEntity>()
                .ForMember(dest => dest.BrandName, map => map.Ignore());

            CreateMap<BeerEntity, BeerDto>();

            CreateMap<BeerModel, BeerEntity>();
            CreateMap<BeerEntity, BeerModel>()
                .ForMember(dest => dest.Id, map => map.Ignore())
                .ForMember(dest => dest.Brand, map => map.Ignore());
        }
    }
}
