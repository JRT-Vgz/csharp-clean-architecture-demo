
using _3___Mappers.Dtos.SaleDtos;
using _4___API.FormValidators.ConceptFormValidators;

namespace _4___API.Tests.FormValidators.Tests.ConceptFormValidators.Tests
{
    public class ConceptUpdateFormValidatorTest
    {
        private readonly ConceptUpdateFormValidator _validator;

        public ConceptUpdateFormValidatorTest()
        {
            _validator = new ConceptUpdateFormValidator();
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptUpdateToIdSaleDto_Id(int id, bool expected, int expectedErrorsCount)
        {
            var conceptUpdateDto = new ConceptUpdateDto
            {
                Id = id,
                IdBeer = 1,
                Quantity = 1,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptUpdateToIdSaleDto_IdBeer(int idBeer, bool expected, int expectedErrorsCount)
        {
            var conceptUpdateDto = new ConceptUpdateDto
            {
                Id = 1,
                IdBeer = idBeer,
                Quantity = 1,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptUpdateToIdSaleDto_Quantity(int quantity, bool expected, int expectedErrorsCount)
        {
            var conceptUpdateDto = new ConceptUpdateDto
            {
                Id = 1,
                IdBeer = 1,
                Quantity = quantity,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptUpdateToIdSaleDto_UnitPrice(int unitPrice, bool expected, int expectedErrorsCount)
        {
            var conceptUpdateDto = new ConceptUpdateDto
            {
                Id = 1,
                IdBeer = 1,
                Quantity = 1,
                UnitPrice = unitPrice
            };

            var result = _validator.Validate(conceptUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }
    }
}
