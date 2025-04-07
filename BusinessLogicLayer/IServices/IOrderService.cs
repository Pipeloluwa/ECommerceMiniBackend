using DomainLayer.DTOs.RequestDTO;
using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IOrderService
    {

        public Task AddOrder(Guid userId, IEnumerable<AddOrderDTO> Id);

        public Task<IEnumerable<CartResponseDto>> GetOrder(Guid userId);

    }
}
