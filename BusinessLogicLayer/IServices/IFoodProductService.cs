using DomainLayer.DTOs;
using DomainLayer.DTOs.RequestDTO;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IFoodProductService
    {
        public Task AddFood(AddFoodProductDTO addFoodProductDTO, Guid userId);
        public Task<IEnumerable<FoodProductResponseDTO>> GetFood();
        public Task<FoodProductResponseDTO?> GetSingleFood(int Id);
        public Task UpdateFood(AddFoodProductDTO addFoodProductDTO, int Id, Guid userId);
        public Task DeleteFood(int Id, Guid userId);
    }
}
