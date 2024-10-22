using _1___Entities;
using _2___Services.Interfaces;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Mappers.BeerMappers
{
    public class BeerDtoMapper : IMapper<BeerEntity, BeerDto>
    {
        public BeerDto Map(BeerEntity beerEntity)
        {
            return new BeerDto
            {
                Id = beerEntity.Id,
                Name = beerEntity.Name,
                BrandName = beerEntity.BrandName,
                Alcohol = beerEntity.Alcohol
            };
        }
    }
}
