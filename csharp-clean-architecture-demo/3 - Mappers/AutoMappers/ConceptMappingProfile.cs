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
            CreateMap<ConceptEntity, ConceptModel>();
            CreateMap<ConceptModel, ConceptEntity>();
            CreateMap<ConceptEntity, ConceptDto>()
                .ForMember(dest => dest.IdConcept, map => map.MapFrom(org => org.Id));
            CreateMap<ConceptUpdateDto, ConceptEntity>();
            CreateMap<ConceptInsertToIdSaleDto, ConceptEntity>();
        }
    }
}
