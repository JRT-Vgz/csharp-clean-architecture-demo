
using _1___Entities;

namespace _3___Mappers.Dtos.SaleDtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<ConceptDto> Concepts { get; set; }
    }
}
