
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Services.BrandService;
using _3___Mappers.Dtos.BrandDtos;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BrandServiceTests
{
    public class GetAllBrandTest
    {
        private readonly Mock<IRepository<BrandEntity>> _brandRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllBrandUseCase<BrandDto> _getAllBrandService;
        public GetAllBrandTest()
        {
            _brandRepositoryMock = new Mock<IRepository<BrandEntity>>();
            _mapperMock = new Mock<IMapper>();
            _getAllBrandService = new GetAllBrandUseCase<BrandDto>(_brandRepositoryMock.Object, _mapperMock.Object);
        }

        private List<BrandEntity> CreateTestEntities()
        {
            return new List<BrandEntity>
            {
                new BrandEntity { Id = 1, Name = "Brand 1" },
                new BrandEntity { Id = 2, Name = "Brand 2" }
            };
        }

        private List<BrandDto> CreateExpectedDtos()
        {
            return new List<BrandDto>
            {
                new BrandDto { Id = 1, Name = "Brand 1" },
                new BrandDto { Id = 2, Name = "Brand 2" }
            };
        }

        [Fact]
        public async Task ServiceReturnsEnumerableDto_Correctly()
        {
            var testEntities = CreateTestEntities();
            var expectedDtos = CreateExpectedDtos();

            _brandRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(testEntities);
            _mapperMock.Setup(m => m.Map<BrandDto>(It.IsAny<BrandEntity>())).Returns((BrandEntity entity) => new BrandDto
            {
                Id = entity.Id,
                Name = entity.Name
            });
            
            var actual = await _getAllBrandService.ExecuteAsync();
            var actualList = actual.ToList();

            Assert.Equal(expectedDtos.Count, actualList.Count);
            for (int i = 0; i < expectedDtos.Count; i++)
            {
                Assert.Equal(expectedDtos[i].Id, actualList[i].Id);
                Assert.Equal(expectedDtos[i].Name, actualList[i].Name);
            }

            _brandRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<BrandDto>(It.IsAny<BrandEntity>()), Times.Exactly(2));
        }
    }
}
