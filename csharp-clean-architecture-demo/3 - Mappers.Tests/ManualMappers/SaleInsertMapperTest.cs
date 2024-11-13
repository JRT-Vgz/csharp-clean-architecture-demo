
using _1___Entities;
using _3___Mappers.Dtos.SaleDtos;
using _3___Mappers.ManualMappers;

namespace _3___Mappers.Tests.ManualMappers
{
    public class SaleInsertMapperTest
    {
        private readonly SaleInsertMapper _mapper;

        public SaleInsertMapperTest()
        {
            _mapper = new SaleInsertMapper();
        }

        [Fact]
        public void MapSaleInsertDto_ToSaleEntity_Correctly()
        {
            var saleInsertDto = new SaleInsertDto
            {
                Concepts = new List<ConceptInsertDto>() 
                { 
                    new ConceptInsertDto {IdBeer = 1, Quantity = 1, UnitPrice = 10 },
                    new ConceptInsertDto {IdBeer = 2, Quantity = 2, UnitPrice = 20 },
                    new ConceptInsertDto {IdBeer = 3, Quantity = 3, UnitPrice = 30 }
                }
            };

            var actual = _mapper.Map(saleInsertDto);

            Assert.NotNull(actual);
            Assert.IsType<SaleEntity>(actual);
            Assert.IsType<List<ConceptEntity>>(actual.Concepts);
            Assert.True(saleInsertDto.Concepts.Count() == actual.Concepts.Count());
            for (int i = 0; i < saleInsertDto.Concepts.Count(); i++)
            {
                Assert.Equal(saleInsertDto.Concepts[i].IdBeer, actual.Concepts[i].IdBeer);
                Assert.Equal(saleInsertDto.Concepts[i].Quantity, actual.Concepts[i].Quantity);
                Assert.Equal(saleInsertDto.Concepts[i].UnitPrice, actual.Concepts[i].UnitPrice);
            }
        }
    }
}
