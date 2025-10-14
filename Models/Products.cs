using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class Products
    {
        public int Article { get; set; }
        public string Name { get; set; }
        public ProductTypes ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public decimal MinimalCost { get; set; }
        public MaterialTypes MaterialType { get; set; }
        public int MaterialTypeId { get; set; }
    }
}
