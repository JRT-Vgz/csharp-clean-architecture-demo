using _1___Entities;
using _3___Data.Models;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Mappers
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile() 
        {
            CreateMap<BrandInsertDto, BrandEntity>();
            CreateMap<BrandUpdateDto, BrandEntity>();

            CreateMap<BrandEntity, BrandDto>();

            CreateMap<BrandModel, BrandEntity>();
            CreateMap<BrandEntity, BrandModel>()
                .ForMember(dest => dest.Id, map => map.Ignore());
        }
    }
}
