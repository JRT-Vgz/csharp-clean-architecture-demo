using _1___Entities;
using _2___Services.BeerService;
using _2___Services.Interfaces;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using Moq;

namespace _4___Unit_Testing
{
    public class BeerServiceTesting
    {
        private readonly Mock<IRepository<BeerEntity>> _mockBeerRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllBeerUseCase<BeerDto> _useCase;

        public BeerServiceTesting()
        {
            _mockBeerRepository = new Mock<IRepository<BeerEntity>>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAllBeerUseCase<BeerDto>(_mockBeerRepository.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task ExecuteAsync_CuandoHayCervezas_RetornaListaDeDtos()
        {
            // Arrange
            var beerEntities = new List<BeerEntity>
        {
            new BeerEntity { Id = 1, Name = "Beer 1", BrandName = "Brand A", Alcohol = 5.0M },
            new BeerEntity { Id = 2, Name = "Beer 2", BrandName = "Brand B", Alcohol = 4.5M }
        };

            var beerDtos = new List<BeerDto>
        {
            new BeerDto { Id = 1, Name = "Beer 1", BrandName = "Brand A", Alcohol = 5.0M },
            new BeerDto { Id = 2, Name = "Beer 2", BrandName = "Brand B", Alcohol = 4.5M }
        };

            _mockBeerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(beerEntities);
            _mockMapper.Setup(m => m.Map<BeerDto>(It.IsAny<BeerEntity>()))
                       .Returns((BeerEntity b) => new BeerDto
                       {
                           Id = b.Id,
                           Name = b.Name,
                           BrandName = b.BrandName,
                           Alcohol = b.Alcohol
                       });

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(beerDtos[0].Id, result.ElementAt(0).Id);
            Assert.Equal(beerDtos[1].Id, result.ElementAt(1).Id);
        }

        [Fact]
        public async Task ExecuteAsync_CuandoNoHayCervezas_RetornaListaVacia()
        {
            // Arrange
            _mockBeerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<BeerEntity>());

            // Act
            var result = await _useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}