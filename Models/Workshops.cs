using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Models.Enums;

namespace WpfApp3.Models
{
    public class Workshops
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WorkshopTypes WorkshopType { get; set; }
        public int StuffCount { get; set; }

        public ICollection<ProductWorkshops> ProductWorkshops { get; set; } = new List<ProductWorkshops>();
    }
}
