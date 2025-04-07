using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.RequestDTO
{
    public class AddOrderDTO
    {
        public int cartId { get; set; }
    }

    public class DeleteOrderDTO
    {
        public int Id { get; set; }
    }


}
