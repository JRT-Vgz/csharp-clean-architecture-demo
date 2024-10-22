using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Mappers.Dtos.BeerDtos
{
    public class BeerInsertDto
    {
        public string Name { get; set; }
        public int IdBrand { get; set; }
        public decimal Alcohol { get; set; }
    }
}
