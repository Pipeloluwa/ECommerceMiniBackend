using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class FoodProductEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }

    }
}
