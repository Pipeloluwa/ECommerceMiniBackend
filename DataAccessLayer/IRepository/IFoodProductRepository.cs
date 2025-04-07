using DomainLayer.DTOs.RequestDTO;
using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IFoodProductRepository
    {
        public Task AddFood(AddFoodProductDTO addFoodProductDTO, Guid userId);
        public Task<IEnumerable<FoodProductEntity>> GetFood();
        public Task<FoodProductEntity?> GetSingleFood(int Id);
        public Task UpdateFood(AddFoodProductDTO addFoodProductDTO, int Id, Guid userId);
        public Task DeleteFood(int Id, Guid userId);
    }
}
