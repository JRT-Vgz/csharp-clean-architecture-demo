using _1___Entities;
using _3___Data.Models;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Mappers
{
    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile() 
        {            
            CreateMap<BeerInsertDto, BeerEntity>();
            CreateMap<BeerUpdateDto, BeerEntity>();

            CreateMap<BeerEntity, BeerDto>();

            CreateMap<BeerModel, BeerEntity>();
            CreateMap<BeerEntity, BeerModel>()
                .ForMember(dest => dest.Id, map => map.Ignore());            
        }
    }
}
