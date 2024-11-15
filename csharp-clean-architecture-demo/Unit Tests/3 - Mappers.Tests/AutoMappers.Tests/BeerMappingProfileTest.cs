
using _1___Entities;
using _3___Data.Models;
using _3___Mappers.AutoMappers;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;

namespace _3___Mappers.Tests.AutoMappers.Tests
{
    public class BeerMappingProfileTest
    {
        private readonly IMapper _mapper;

        public BeerMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BeerMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void MapBeerInsertDto_ToBeerEntity_Correctly()
        {
            var beerInsertDto = new BeerInsertDto() { Name = "Beer", IdBrand = 1, Alcohol = 4.5m };

            var actual = _mapper.Map<BeerEntity>(beerInsertDto);

            Assert.IsType<BeerEntity>(actual);
            Assert.Equal(0, actual.Id);
            Assert.Equal(beerInsertDto.Name, actual.Name);
            Assert.Equal(beerInsertDto.IdBrand, actual.IdBrand);
            Assert.Equal(beerInsertDto.Alcohol, actual.Alcohol);
        }

        [Fact]
        public void MapBeerUpdateDto_ToBeerEntity_Correctly()
        {
            var beerUpdateDto = new BeerUpdateDto() { Id = 1, Name = "Beer", IdBrand = 1, Alcohol = 4.5m };

            var actual = _mapper.Map<BeerEntity>(beerUpdateDto);

            Assert.IsType<BeerEntity>(actual);
            Assert.Equal(beerUpdateDto.Id, actual.Id);
            Assert.Equal(beerUpdateDto.Name, actual.Name);
            Assert.Equal(beerUpdateDto.IdBrand, actual.IdBrand);
            Assert.Equal(beerUpdateDto.Alcohol, actual.Alcohol);
        }

            [Fact]
        public void MapBeerEntity_ToBeerDto_Correctly()
        {
            var beerEntity = new BeerEntity() { Id = 1, Name = "Beer", IdBrand = 1, BrandName = "Brand", Alcohol = 4.5m };

            var actual = _mapper.Map<BeerDto>(beerEntity);

            Assert.IsType<BeerDto>(actual);
            Assert.Equal(beerEntity.Id, actual.Id);
            Assert.Equal(beerEntity.Name, actual.Name);
            Assert.Equal(beerEntity.BrandName, actual.BrandName);
            Assert.Equal(beerEntity.Alcohol, actual.Alcohol);
        }

        [Fact]
        public void MapBeerModel_ToBeerEntity_Correctly()
        {
            var beerModel = new BeerModel() { Id = 1, Name = "Beer", IdBrand = 1, Alcohol = 4.5m };

            var actual = _mapper.Map<BeerEntity>(beerModel);

            Assert.IsType<BeerEntity>(actual);
            Assert.Equal(beerModel.Id, actual.Id);
            Assert.Equal(beerModel.Name, actual.Name);
            Assert.Equal(beerModel.IdBrand, actual.IdBrand);
            Assert.Equal(beerModel.Alcohol, actual.Alcohol);
        }

        [Fact]
        public void MapEntity_ToBeerModel_Correctly()
        {
            var beerEntity = new BeerEntity() { Id = 1, Name = "Beer", IdBrand = 1, Alcohol = 4.5m };

            var actual = _mapper.Map<BeerModel>(beerEntity);

            Assert.IsType<BeerModel>(actual);
            Assert.NotEqual(beerEntity.Id, actual.Id);
            Assert.Equal(beerEntity.Name, actual.Name);
            Assert.Equal(beerEntity.IdBrand, actual.IdBrand);
            Assert.Equal(beerEntity.Alcohol, actual.Alcohol);
        }
    }
}
   
