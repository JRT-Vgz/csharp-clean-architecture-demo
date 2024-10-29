
using _1___Entities;
using _2___Services._Interfaces;
using _3___Mappers.Dtos.SaleDtos;

namespace _3___Mappers.ManualMappers
{
    public class SaleInsertMapper : IManualMapper<SaleInsertDto, SaleEntity>
    {
        public SaleEntity Map(SaleInsertDto saleInsertDto)
        {
            var conceptEntities = new List<ConceptEntity>();

            foreach (var conceptDto in saleInsertDto.Concepts)
            {
                conceptEntities.Add(new ConceptEntity(conceptDto.IdBeer, conceptDto.Quantity, conceptDto.UnitPrice));
            }

            return new SaleEntity(DateTime.Now, conceptEntities);
        }
    }
}
