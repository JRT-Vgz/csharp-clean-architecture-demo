
using _3___Mappers.Dtos.SaleDtos;
using _4___API.FormValidators.SaleFormValidators;

namespace _4___API.Tests.FormValidators.Tests.SaleFormValidators.Tests
{
    public class SaleInsertFormValidatorTest
    {
        private readonly SaleInsertFormValidator _validator;

        public SaleInsertFormValidatorTest()
        {
            _validator = new SaleInsertFormValidator();
        }

        [Fact]
        public void ValidateFalse_WhenSaleHasNoContepts()
        {
            var saleInsertDto = new SaleInsertDto { Concepts = new List<ConceptInsertDto>() };

            var result = _validator.Validate(saleInsertDto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Concepts"
                            && e.ErrorMessage == "El campo 'Concepts' no puede estar vacío.");
        }

        [Fact]
        public void ValidateTrue_WhenSaleHasFullfilledConcept()
        {
            var saleInsertDto = new SaleInsertDto
            {
                Concepts = new List<ConceptInsertDto>
                {
                    new ConceptInsertDto { IdBeer = 1, Quantity = 1, UnitPrice = 1},
                }
            };

            var result = _validator.Validate(saleInsertDto);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(0, 0, 1, 2)]
        [InlineData(1, 0, 0, 2)]
        [InlineData(0, 1, 0, 2)]
        [InlineData(0, 0, 0, 3)]
        public void ValidateFalse_WhenConceptLacksInformation(int idBeer, int quantity, decimal unitPrice,
            int expectedErrorsCount)
        {
            var saleInsertDto = new SaleInsertDto
            {
                Concepts = new List<ConceptInsertDto>
                {
                    new ConceptInsertDto { IdBeer = idBeer, Quantity = quantity, UnitPrice = unitPrice},
                }
            };

            var result = _validator.Validate(saleInsertDto);

            Assert.False(result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }
    }
}
