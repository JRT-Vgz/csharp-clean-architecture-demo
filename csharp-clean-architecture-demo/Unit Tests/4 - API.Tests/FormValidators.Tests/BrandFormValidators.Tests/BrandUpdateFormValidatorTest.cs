
using _3___Mappers.Dtos.BrandDtos;
using _4___API.FormValidators.BrandValidators;
using FluentValidation;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace _4___API.Tests.FormValidators.Tests.BrandFormValidators.Tests
{
    public class BrandUpdateFormValidatorTest
    {
        private readonly BrandUpdateFormValidator _validator;

        public BrandUpdateFormValidatorTest()
        {
            _validator = new BrandUpdateFormValidator();
        }

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(0, false, 2)]
        [InlineData(null, false, 2)]
        [InlineData(-1, false, 2)]
        public void ValidateBrandUpdateDto_Id(int id, bool expected, int expectedErrorsCount)
        {
            var brandUpdateDto = new BrandUpdateDto { Id = id, Name = "Brand" };

            var result = _validator.Validate(brandUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);

            if (!result.IsValid)
            {
                if (id == 0 || id == null)
                {
                    Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "El campo 'Id' es obligatorio.");
                }
                else if (int.IsNegative(id))
                {
                    Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "El campo 'Id' no puede ser negativo.");
                }           
            }
        }

        [Theory]
        [InlineData("Brand", true, 0)]
        [InlineData("", false, 1)]
        [InlineData(null, false, 1)]
        [InlineData("BrandBrandBrandBrandBrandBrandBrand", false, 1)]
        [InlineData("Brand@", false, 1)]
        [InlineData("BrandBrandBrandBrandBrandBrandBrand@", false, 2)]
        public void ValidateBrandUpdateDto_Name(string name, bool expected, int expectedErrorsCount)
        {
            var brandUpdateDto = new BrandUpdateDto { Id = 1, Name = name };

            var result = _validator.Validate(brandUpdateDto);

            Assert.Equal(expected, result.IsValid);
            Assert.Equal(expectedErrorsCount, result.Errors.Count);
        }
    }
}
