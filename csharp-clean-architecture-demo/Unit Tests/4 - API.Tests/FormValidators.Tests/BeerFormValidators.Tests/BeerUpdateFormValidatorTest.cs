
using _3___Mappers.Dtos.BeerDtos;
using _4___API.FormValidators.BeerValidators;

namespace _4___API.Tests.FormValidators.Tests.BeerFormValidators.Tests
{
    public class BeerUpdateFormValidatorTest
    {
        private readonly BeerUpdateFormValidator _validator;

        public BeerUpdateFormValidatorTest()
        {
            _validator = new BeerUpdateFormValidator();
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 2)]
        [InlineData(null, false, 2)]
        [InlineData(-1, false, 2)]
        public void ValidateBeerUpdateDto_Id(int id, bool expected, int expectedErrorsCount)
        {
            var beerUpdateDto = new BeerUpdateDto { Id = id, Name = "Beer", IdBrand = 1, Alcohol = 4.5m };

            var result = _validator.Validate(beerUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

            [Theory]
        [InlineData("Beer", true, 0)]
        [InlineData("", false, 1)]
        [InlineData(null, false, 1)]
        [InlineData("BeerBeerBeerBeerBeerBeerBeerBeer", false, 1)]
        [InlineData("Beer@", false, 1)]
        [InlineData("BeerBeerBeerBeerBeerBeerBeerBeer@@", false, 2)]
        public void ValidateBeerInsertDto_Name(string name, bool expected, int expectedErrorsCount)
        {
            var beerUpdateDto = new BeerUpdateDto { Id = 1, Name = name, IdBrand = 1, Alcohol = 4.5m };

            var result = _validator.Validate(beerUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count());
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 1)]

        public void ValidateBeerInsertDto_IdBrand(int idBrand, bool expected, int expectedErrorsCount)
        {
            var beerUpdateDto = new BeerUpdateDto { Id = 1 , Name = "Beer", IdBrand = idBrand, Alcohol = 4.5m };

            var result = _validator.Validate(beerUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count());
        }

        [Theory]
        [InlineData(4.5, true, 0)]
        [InlineData(0, true, 0)]
        [InlineData(100, true, 0)]
        [InlineData(100.05, false, 1)]
        [InlineData(-1, false, 1)]
        public void ValidateBeerInsertDto_Alcohol(decimal alcohol, bool expected, int expectedErrorsCount)
        {
            var beerUpdateDto = new BeerUpdateDto { Id = 1, Name = "Beer", IdBrand = 1, Alcohol = alcohol };

            var result = _validator.Validate(beerUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count());
        }
    }
}
