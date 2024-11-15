
using _1___Entities;
using _3___Data.Models;
using _3___Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace _3___Repositories.Tests
{
    public class BeerRepositoryTest
    {
        private readonly Mock<BreweryContext> _contextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BeerRepository _beerRepository;

        public BeerRepositoryTest()
        {
            var beerModels = CreateTestBeerModelList();

            _contextMock = new Mock<BreweryContext>();
            _contextMock.Setup<DbSet<BeerModel>>(c => c.Beers).ReturnsDbSet(beerModels);
            _contextMock.Setup<DbSet<BrandModel>>(c => c.Brands).ReturnsDbSet(CreateTestBrandModelList());

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.Map<BrandEntity>(It.IsAny<BrandModel>())).Returns(new BrandEntity());
            foreach (var beerModel in beerModels)
            {
                _mapperMock.Setup(m => m.Map<BeerEntity>(beerModel)).Returns(new BeerEntity
                {
                    Id = beerModel.Id,
                    Name = beerModel.Name,
                    Alcohol = beerModel.Alcohol,
                    IdBrand = beerModel.IdBrand
                });
            }
            _beerRepository = new BeerRepository(_contextMock.Object, _mapperMock.Object);
        }
        private static List<BrandModel> CreateTestBrandModelList()
            => new List<BrandModel>
            {
                new BrandModel { Id = 1, Name = "Brand 1" },
                new BrandModel { Id = 2, Name = "Brand 2" },
                new BrandModel { Id = 3, Name = "Brand 3" },
                new BrandModel { Id = 4, Name = "Brand 4" }
            };

        private static List<BeerModel> CreateTestBeerModelList()
            => new List<BeerModel>
            {               
                new BeerModel { Id = 2, Name = "Beer 2", Alcohol = 5m, IdBrand = 4},
                new BeerModel { Id = 3, Name = "Beer 3", Alcohol = 5m, IdBrand = 1},
                new BeerModel { Id = 1, Name = "Beer 1", Alcohol = 5m, IdBrand = 3},
                new BeerModel { Id = 4, Name = "Beer 4", Alcohol = 5m, IdBrand = 2}
            };

        [Fact]
        public async Task ReturnBeerEntities_OrderedById_Correctly()
        {
            var orderedBeerModels = CreateTestBeerModelList().OrderBy(b => b.Id).ToList();

            var actual = await _beerRepository.GetAllAsync();
            var actualList = actual.ToList();            

            Assert.NotNull(actualList);
            Assert.IsType<List<BeerEntity>>(actualList);
            Assert.True(actualList.Count == orderedBeerModels.Count);
            for (int i = 0; i < actualList.Count; i++)
            {
                Assert.Equal(orderedBeerModels[i].Id, actualList[i].Id);
                Assert.Equal(orderedBeerModels[i].Name, actualList[i].Name);
            }

            _mapperMock.Verify(m => m.Map<BeerEntity>(It.IsAny<BeerModel>()), Times.Exactly(4));
        }
    }
}
