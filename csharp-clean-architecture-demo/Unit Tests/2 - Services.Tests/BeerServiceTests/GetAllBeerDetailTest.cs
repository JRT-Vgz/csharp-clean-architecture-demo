
using _1___Entities;
using _2___Services._Interfaces;
using _2___Services.Services.BeerService;
using _3___Mappers.Dtos.BeerDtos;
using _3___Presenters.ViewModels;
using AutoMapper;
using Moq;

namespace _2___Services.Tests.BeerServiceTests
{
    public class GetAllBeerDetailTest
    {
        private readonly Mock<IRepository<BeerEntity>> _beerRepositoryMock;
        private readonly Mock<IPresenter<BeerEntity, BeerDetailViewModel>> _presenterMock;
        private readonly GetAllBeerDetailUseCase<BeerDetailViewModel> _getAllBeerDetailUseCase;

        public GetAllBeerDetailTest()
        {
            _beerRepositoryMock = new Mock<IRepository<BeerEntity>>();
            _presenterMock = new Mock<IPresenter<BeerEntity, BeerDetailViewModel>>();
            _getAllBeerDetailUseCase = new GetAllBeerDetailUseCase<BeerDetailViewModel>(
                _beerRepositoryMock.Object,
                _presenterMock.Object);
        }

        private List<BeerEntity> CreateTestEntities()
        {
            return new List<BeerEntity>
            {
                new BeerEntity { Id = 1, Name = "Beer 1" },
                new BeerEntity { Id = 2, Name = "Beer 2" }
            };
        }

        [Fact]
        public async Task ServiceReturnsListViewModels_Correctly()
        {
            var entities = CreateTestEntities();

            _beerRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _presenterMock.Setup(p => p.Present(It.IsAny<BeerEntity>())).Returns(new BeerDetailViewModel());

            var actual = await _getAllBeerDetailUseCase.ExecuteAsync();
            var actualList = actual.ToList();

            for (var i = 0; i < actualList.Count; i++)
            {
                Assert.NotNull(actualList[i]);
                Assert.IsType<BeerDetailViewModel>(actualList[i]);
            }

            _beerRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once());
            _presenterMock.Verify(p => p.Present(It.IsAny<BeerEntity>()), Times.Exactly(2));
        }
    }

}
