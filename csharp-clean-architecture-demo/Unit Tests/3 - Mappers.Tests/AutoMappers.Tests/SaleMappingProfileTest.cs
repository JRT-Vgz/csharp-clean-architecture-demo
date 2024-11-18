
using _1___Entities;
using _3___Mappers.AutoMappers;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;

namespace _3___Mappers.Tests.AutoMappers.Tests
{
    public class SaleMappingProfileTest
    {
        private readonly IMapper _mapper;

        public SaleMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SaleMappingProfile>();
                cfg.AddProfile<ConceptMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        private SaleEntity CreateTestSaleEntity()
        {
            var concepts = new List<ConceptEntity>()
            {
                new ConceptEntity(1,1,10),
                new ConceptEntity(2,2,20),
                new ConceptEntity(3,3,30)
            };

            return new SaleEntity(DateTime.Now, concepts);
        }

        [Fact]
        public void MapSaleEntity_ToSaleDto_Correctly()
        {
            var saleEntity = CreateTestSaleEntity();

            var actual = _mapper.Map<SaleDto>(saleEntity);

            Assert.IsType<SaleDto>(actual);
            Assert.IsType<List<ConceptDto>>(actual.Concepts);
            Assert.True(actual.Concepts.Count() == saleEntity.Concepts.Count());
            Assert.Equal(saleEntity.Id, actual.Id);
            Assert.Equal(saleEntity.Date, actual.Date);
            Assert.Equal(saleEntity.Total, actual.Total);
        }
    }
}
