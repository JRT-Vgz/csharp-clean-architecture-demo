
using _3___Mappers.Dtos.BrandDtos;
using _4___API.FormValidators.BrandValidators;
using MySqlX.XDevAPI.Common;
using System.Text.RegularExpressions;

namespace _4___API.Tests.FormValidators.Tests.BrandFormValidators.Tests
{
    public class BrandInsertFormValidatorTest
    {
        private readonly BrandInsertFormValidator _validator;

        public BrandInsertFormValidatorTest()
        {
            _validator = new BrandInsertFormValidator();
        }

        [Theory]
        [InlineData("Brand", true, 0)]
        [InlineData("", false, 1)]
        [InlineData(null, false, 1)]
        [InlineData("BrandBrandBrandBrandBrandBrandBrand", false, 1)]
        [InlineData("Brand@", false, 1)]
        [InlineData("BrandBrandBrandBrandBrandBrandBrand@", false, 2)]
        public void ValidateBrandInsertDto_Name(string name, bool expected, int expectedErrorsCount)
        {
            var brandInsertDto = new BrandInsertDto { Name = name };

            var result = _validator.Validate(brandInsertDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }
    }
}
