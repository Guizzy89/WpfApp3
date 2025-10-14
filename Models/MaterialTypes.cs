using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class MaterialTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal LosePercent { get; set; }

        public ICollection<Products> Products { get; set; } = new List<Products>(); // Теперь Products сразу содержит пустой List<Products>, и можно безопасно добавлять элементы.
    }
}
