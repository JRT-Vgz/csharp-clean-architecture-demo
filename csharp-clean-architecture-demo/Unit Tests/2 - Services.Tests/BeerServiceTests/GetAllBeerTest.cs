
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class GetAllBeerTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllBeerUseCase<BeerDto> _getAllBeerUseCase;

        public GetAllBeerTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _mapperMock = new Mock<IMapper>();
            _getAllBeerUseCase = new GetAllBeerUseCase<BeerDto>(
                _beerRepositoryMock.Object,
                _mapperMock.Object);
        }

        private List<BeerEntity> CreateTestEntities()
        {
            return new List<BeerEntity>
            {
                new BeerEntity { Id = 1, Name = "Beer 1" },
                new BeerEntity { Id = 2, Name = "Beer 2" }
            };
        }

        private List<BeerDto> CreateExpectedDtos()
        {
            return new List<BeerDto>
            {
                new BeerDto { Id = 1, Name = "Beer 1" },
                new BeerDto { Id = 2, Name = "Beer 2" }
            };
        }

        [Fact]
        public async Task ServiceReturnsListDto_Correctly()
        {
            var entities = CreateTestEntities();
            var expectedDtos = CreateExpectedDtos();

            _beerRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<BeerDto>(It.IsAny<BeerEntity>())).Returns((BeerEntity entity) => new BeerDto()
            {
                Id = entity.Id,
                Name = entity.Name
            });

            var actual = await _getAllBeerUseCase.ExecuteAsync();
            var actualList = actual.ToList();

            for ( var i = 0; i < expectedDtos.Count; i++)
            {
                Assert.NotNull(actualList[i]);
                Assert.IsType<BeerDto>(actualList[i]);
                Assert.Equal(expectedDtos[i].Id, actualList[i].Id);
                Assert.Equal(expectedDtos[i].Name, actualList[i].Name);
            }

            _beerRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once());
            _mapperMock.Verify(m => m.Map<BeerDto>(It.IsAny<BeerEntity>()), Times.Exactly(2));
        }
    }
}
