using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Coefficient { get; set; }

        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
