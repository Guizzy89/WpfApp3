using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class ProductWorkshops
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Workshops Workshop { get; set; }
        public int WorkshopId { get; set; }
        public decimal ManufacturingInHours { get; set; }
    }
}
