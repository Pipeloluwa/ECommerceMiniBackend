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
    public interface ICartService
    {

        public Task AddCart(AddCartDTO addCartDTO, Guid userId);
        public Task<IEnumerable<CartResponseDto>> GetCart(Guid userId);
        public  Task<CartResponseDto> GetSingleCart(int Id, Guid userId);
        public  Task UpdateCart(UpdateCartDTO updateCartDTO, Guid userId);
        public Task DeleteCart(IEnumerable<int> Id, Guid userId);
    }
}
