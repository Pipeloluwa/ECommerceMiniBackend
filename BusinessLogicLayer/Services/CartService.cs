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
    public class CartService(
            ICartRepository cartRepository,
            IFoodProductRepository foodProductRepository
        ): ICartService
    {
        private readonly ICartRepository _cartRepository= cartRepository;
        private readonly IFoodProductRepository _foodProductRepository= foodProductRepository;



        public async Task AddCart(AddCartDTO addCartDTO, Guid userId)
        {

            FoodProductEntity? foodProductEntity = await _foodProductRepository.GetSingleFood(addCartDTO.FoodId);
            if (foodProductEntity == null)
            {
                throw new HttpRequestException("This product does no longer exist or sold off");
            }

            await _cartRepository.AddCart(addCartDTO, userId, (decimal)foodProductEntity.Price, 1);
        }



        public async Task<IEnumerable<CartResponseDto>> GetCart(Guid userId)
        {
            return await _cartRepository.GetCart(userId);
        }


        public async Task<CartResponseDto> GetSingleCart(int Id, Guid userId)
        {
            return await _cartRepository.GetSingleCart(Id, userId);
        }


        public async Task UpdateCart(UpdateCartDTO updateCartDTO, Guid userId)
        {
            CartResponseDto? cartResponseDto = await _cartRepository.GetSingleCart(updateCartDTO.Id, userId);

            FoodProductEntity? foodProductEntity = await _foodProductRepository.GetSingleFood(updateCartDTO.FoodId);
            if (foodProductEntity== null || cartResponseDto == null)
            {
                throw new HttpRequestException("This product does no longer exist or sold off");
            }


            decimal newTotalPrice = (decimal)(foodProductEntity.Price * updateCartDTO.Quantity);

            await _cartRepository.UpdateCart(
                updateCartDTO.Id,
                userId,
                (decimal)(cartResponseDto.TotalPrice + newTotalPrice),
                (int)cartResponseDto.Quantity + updateCartDTO.Quantity
                );
        }

        public async Task DeleteCart(IEnumerable<int> Id, Guid userId)
        {
           foreach (int id in Id)
            {
                await _cartRepository.DeleteCart(id, userId);
            }
        }

    }
}
