
using _1___Entities;
using _3___Mappers.AutoMappers;
using _3___Mappers.Dtos.SaleDtos;
using AutoMapper;

namespace _3___Mappers.Tests.AutoMappers.Tests
{
    public class ConceptMappingProfileTest
    {
        private readonly IMapper _mapper;

        public ConceptMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ConceptMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void MapConceptEntity_ToConceptDto_Correctly()
        {
            int id = 1;
            int idBeer = 2;
            int quantity = 3;
            decimal unitPrice = 10;
            var conceptEntity = new ConceptEntity(id, idBeer, quantity, unitPrice);

            var actual = _mapper.Map<ConceptDto>(conceptEntity);

            Assert.IsType<ConceptDto>(actual);
            Assert.Equal(conceptEntity.Id, actual.IdConcept);
            Assert.Equal(conceptEntity.IdBeer, actual.IdBeer);
            Assert.Equal(conceptEntity.Quantity, actual.Quantity);
            Assert.Equal(conceptEntity.UnitPrice, actual.UnitPrice);
            Assert.True(actual.ConceptPrice == conceptEntity.Quantity * conceptEntity.UnitPrice);
        }
    }
}
