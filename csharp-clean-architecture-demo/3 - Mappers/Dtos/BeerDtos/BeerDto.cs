using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Mappers.Dtos.BeerDtos
{
    public class BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Alcohol { get; set; }
    }
}