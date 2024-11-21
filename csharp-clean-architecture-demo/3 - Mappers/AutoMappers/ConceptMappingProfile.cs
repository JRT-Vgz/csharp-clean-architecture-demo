using _1___Entities;
using _3___Data.Models;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;

namespace _3___Mappers.AutoMappers
{
    public class ConceptMappingProfile : Profile
    {
        public ConceptMappingProfile()
        {
            CreateMap<ConceptEntity, ConceptModel>()
                .ForMember(dest => dest.Sale, map => map.Ignore());

            CreateMap<ConceptModel, ConceptEntity>()
                .ForMember(dest => dest.ConceptPrice, map => map.Ignore());

            CreateMap<ConceptEntity, ConceptDto>()
                .ForMember(dest => dest.IdConcept, map => map.MapFrom(org => org.Id));

            CreateMap<ConceptUpdateDto, ConceptEntity>()
                .ForMember(dest => dest.IdSale, map => map.Ignore())
                .ForMember(dest => dest.ConceptPrice, map => map.Ignore());

            CreateMap<ConceptInsertToIdSaleDto, ConceptEntity>()
                .ForMember(dest => dest.Id, map => map.Ignore())
                .ForMember(dest => dest.ConceptPrice, map => map.Ignore());
        }
    }
}
