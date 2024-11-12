
using _1___Entities;

namespace Entities.Tests
{
    public class BeerEntityTest
    {
        [Theory]
        [InlineData(7, false)]
        [InlineData(7.49, false)]
        [InlineData(7.5, true)]
        [InlineData(10, true)]
        public void DetermineIfBeerIsStrong(decimal alcohol, bool expected)
        {
            var beerEntity = new BeerEntity() { Alcohol = alcohol };

            bool actual = beerEntity.IsStrongBeer();

            Assert.Equal(expected, actual);
        }
    }
}
