using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3___Data.Models
{
    public class BeerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Alcohol { get; set; }
        public int IdBrand { get; set; }

        [ForeignKey("IdBrand")]
        public BrandModel Brand { get; set; }
    }
}
