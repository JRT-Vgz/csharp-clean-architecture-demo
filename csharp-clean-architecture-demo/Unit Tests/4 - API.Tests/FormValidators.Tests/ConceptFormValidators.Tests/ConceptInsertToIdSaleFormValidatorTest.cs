
using _3___Mappers.Dtos.SaleDtos;
using _4___API.FormValidators.ConceptFormValidators;

namespace _4___API.Tests.FormValidators.Tests.ConceptFormValidators.Tests
{
    public class ConceptInsertToIdSaleFormValidatorTest
    {
        private readonly ConceptInsertToIdSaleFormValidator _validator;

        public ConceptInsertToIdSaleFormValidatorTest()
        {
            _validator = new ConceptInsertToIdSaleFormValidator();
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptInsertToIdSaleDto_IdSale(int idSale, bool expected, int expectedErrorsCount)
        {
            var conceptInsertToIdSaleDto = new ConceptInsertToIdSaleDto
            {
                IdSale = idSale,
                IdBeer = 1,
                Quantity = 1,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptInsertToIdSaleDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptInsertToIdSaleDto_IdBeer(int idBeer, bool expected, int expectedErrorsCount)
        {
            var conceptInsertToIdSaleDto = new ConceptInsertToIdSaleDto
            {
                IdSale = 1,
                IdBeer = idBeer,
                Quantity = 1,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptInsertToIdSaleDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptInsertToIdSaleDto_Quantity(int quantity, bool expected, int expectedErrorsCount)
        {
            var conceptInsertToIdSaleDto = new ConceptInsertToIdSaleDto
            {
                IdSale = 1,
                IdBeer = 1,
                Quantity = quantity,
                UnitPrice = 1
            };

            var result = _validator.Validate(conceptInsertToIdSaleDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 1)]
        [InlineData(null, false, 1)]
        [InlineData(-1, false, 2)]
        public void ValidateConceptInsertToIdSaleDto_UnitPrice(int unitPrice, bool expected, int expectedErrorsCount)
        {
            var conceptInsertToIdSaleDto = new ConceptInsertToIdSaleDto
            {
                IdSale = 1,
                IdBeer = 1,
                Quantity = 1,
                UnitPrice = unitPrice
            };

            var result = _validator.Validate(conceptInsertToIdSaleDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }
    }
}
