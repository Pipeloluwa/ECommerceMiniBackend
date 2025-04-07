using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IOrderRepository
    {

        public Task AddOrder(Guid userId, int foodId, int quantity, decimal totalPrice);

        public Task<IEnumerable<CartResponseDto>> GetOrder(Guid userId);

    }
}
