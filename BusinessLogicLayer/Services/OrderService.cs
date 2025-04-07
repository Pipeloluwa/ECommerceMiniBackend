using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DomainLayer.DTOs.RequestDTO;
using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OrderService(
            DataAccessLayer.IRepository.IOrderRepository orderRepository,
            ICartRepository cartRepository
        ): IServices.IOrderService
    {

        private readonly DataAccessLayer.IRepository.IOrderRepository _orderRepository= orderRepository;
        private readonly ICartRepository _cartRepository= cartRepository;

        public async Task AddOrder(Guid userId, IEnumerable<AddOrderDTO> addOrderDTOs)
        {
            foreach (var addOrderDTO in addOrderDTOs)
            {
                CartResponseDto? cartResponseDto = await _cartRepository.GetSingleCart(addOrderDTO.cartId, userId);

                if (cartResponseDto == null)
                {
                    throw new HttpRequestException("this cart item does not exist");
                }

                await _orderRepository.AddOrder(
                    userId,
                    (int)cartResponseDto.FoodId,
                    (int)cartResponseDto.Quantity,
                    (decimal)cartResponseDto.TotalPrice
                );
            }
            
            
        }



        public async Task<IEnumerable<CartResponseDto>> GetOrder(Guid userId)
        {

           return await _orderRepository.GetOrder(userId);
            
        }


    }
}
