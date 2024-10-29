
namespace _3___Mappers.Dtos.SaleDtos
{
    public class SaleInsertDto
    {
        public List<ConceptInsertDto> Concepts { get; set; }
    }

    public class ConceptInsertDto
    {
        public int IdBeer { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
