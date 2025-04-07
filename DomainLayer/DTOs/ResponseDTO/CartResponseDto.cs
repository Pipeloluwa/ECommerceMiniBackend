using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.ResponseDTO
{
    public class CartResponseDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? FoodId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
