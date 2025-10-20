using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // ручной ввод
        public int Article { get; set; }
        public string Name { get; set; }
        public ProductTypes ProductType { get; set; }   // Навигационное свойство к типу продукта
        public int ProductTypeId { get; set; }
        public decimal MinimalCost { get; set; }
        public MaterialTypes MaterialType { get; set; }
        public int MaterialTypeId { get; set; }
    }
}
