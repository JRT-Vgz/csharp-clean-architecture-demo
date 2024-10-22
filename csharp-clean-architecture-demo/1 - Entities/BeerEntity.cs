namespace _1___Entities
{
    public class BeerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdBrand { get; set; }
        public string BrandName { get; set; }
        public decimal Alcohol { get; set; }

        public bool IsStrongBeer()
            => Alcohol >= 7.5m;
    }
}
