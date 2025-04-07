using AutoMapper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DomainLayer.DTOs;
using DomainLayer.DTOs.RequestDTO;


namespace BusinessLogicLayer.Services
{
    public class FoodProductService(
            IMapper mapper,
            IFoodProductRepository foodProductRepository
        ): IFoodProductService
    {
        private readonly IMapper _mapper= mapper;
        private readonly IFoodProductRepository _foodProductRepository = foodProductRepository;


        public async Task AddFood(AddFoodProductDTO addFoodProductDTO, Guid userId)
        {
            try
            {
                await _foodProductRepository.AddFood(addFoodProductDTO, userId);
            }
            catch (Exception e)
            {
                throw new HttpRequestException("Could not create");
            }
            
        }

        public async Task DeleteFood(int Id, Guid userId)
        {
           await _foodProductRepository.DeleteFood(Id, userId);
        }

        public async Task<IEnumerable<FoodProductResponseDTO>> GetFood()
        {
            return _mapper.Map<IEnumerable<FoodProductResponseDTO>>(await _foodProductRepository.GetFood());
        }

        public async Task<FoodProductResponseDTO?> GetSingleFood(int Id)
        {
            return _mapper.Map<FoodProductResponseDTO>(await _foodProductRepository.GetSingleFood(Id));
        }

        public async Task UpdateFood(AddFoodProductDTO addFoodProductDTO, int Id, Guid userId)
        {
            await _foodProductRepository.UpdateFood(addFoodProductDTO, Id, userId);   
        }
    }
}
