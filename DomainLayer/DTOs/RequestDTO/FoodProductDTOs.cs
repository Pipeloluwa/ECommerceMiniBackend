using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.RequestDTO
{
    public class AddFoodProductDTO
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Image { get; set; }
    }
}
