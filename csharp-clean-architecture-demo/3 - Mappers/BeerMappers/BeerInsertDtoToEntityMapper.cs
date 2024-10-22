using _1___Entities;
using _2___Services.Interfaces;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;

namespace _3___Mappers.BeerMappers
{
    public class BeerInsertDtoToEntityMapper : IMapper<BeerInsertDto, BeerEntity>
    {
        public BeerEntity Map(BeerInsertDto beerInsertDto)
            => new BeerEntity
            {
                Name = beerInsertDto.Name,
                IdBrand = beerInsertDto.IdBrand,
                Alcohol = beerInsertDto.Alcohol
            };
    }
}
