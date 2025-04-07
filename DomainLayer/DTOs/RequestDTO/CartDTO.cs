using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.RequestDTO
{
    public class AddCartDTO
    {
        public int FoodId { get; set; }
    }

    public class UpdateCartDTO
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
    }

    public class DeleteCartDTO
    {
        public int Id { get; set; }
    }
}
