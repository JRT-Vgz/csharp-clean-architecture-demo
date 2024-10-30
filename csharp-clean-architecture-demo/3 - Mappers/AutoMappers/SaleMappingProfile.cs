
using _1___Entities;
using _3___Data.Models;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;

namespace _3___Mappers.AutoMappers
{
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            CreateMap<SaleEntity, SaleModel>()
            .ForMember(dest => dest.Concepts, opt => opt.MapFrom(src => src.Concepts));

            CreateMap<SaleModel, SaleEntity>()
                .ForMember(dest => dest.Concepts, opt => opt.MapFrom(src => src.Concepts));

            CreateMap<SaleEntity, SaleDto>()
                .ForMember(dest => dest.Concepts, opt => opt.MapFrom(src => src.Concepts));
        }
    }
}
