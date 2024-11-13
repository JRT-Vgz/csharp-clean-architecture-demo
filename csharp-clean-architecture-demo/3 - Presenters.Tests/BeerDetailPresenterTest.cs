
using _1___Entities;
using _3___Presenters.ViewModels;

namespace _3___Presenters.Tests
{
    public class BeerDetailPresenterTest
    {
        private readonly BeerDetailPresenter _presenter;

        public BeerDetailPresenterTest()
        {
            _presenter = new BeerDetailPresenter();
        }

        [Theory]
        [InlineData(8.5, "Red", "Strong Beer")]
        [InlineData(4.5, "Green", "Smooth Beer")]
        public void Present_Strong_BeerDetailViewModel_FromBeerEntity_Correctly(decimal alcohol, string color, string message)
        {
            var beerEntity = new BeerEntity
            {
                Name = "Beer",
                BrandName = "Brand",
                Alcohol = alcohol
            };

            var actual = _presenter.Present(beerEntity);

            Assert.NotNull(actual);
            Assert.IsType<BeerDetailViewModel>(actual);
            Assert.Equal($"Cerveza: {beerEntity.Name}", actual.Name);
            Assert.Equal($"Marca: {beerEntity.BrandName}", actual.BrandName);
            Assert.Equal($"{beerEntity.Alcohol}% de alcohol", actual.Alcohol);
            Assert.Equal(color, actual.Color);
            Assert.Equal(message, actual.Message);
        }
    }
}
