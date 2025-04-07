using Dapper;
using DataAccessLayer.Dapper;
using DataAccessLayer.IRepository;
using DomainLayer.DTOs.RequestDTO;
using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLayer.Repository
{
    public class FoodProductRepository(
        IDapperConnection dapperConnection
        ) : IFoodProductRepository
    {
        private readonly IDapperConnection _dapperConnection = dapperConnection;

        public async Task AddFood(AddFoodProductDTO addFoodProductDTO, Guid userId)
        {
            var parameters = new { Name = addFoodProductDTO.Name, Price = addFoodProductDTO.Price, Image = addFoodProductDTO.Image, UserId= userId };

            await _dapperConnection.Execute(parameters, "AddProduct", CommandType.StoredProcedure);
        }

        public async Task DeleteFood(int Id, Guid userId)
        {
            var parameters = new { FoodId = Id, UserId= userId };

            await _dapperConnection.Execute(parameters, "DeleteFoodProduct", CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<FoodProductEntity>> GetFood()
        {

            IEnumerable<FoodProductEntity> result = await _dapperConnection.QueryAll<FoodProductEntity>(null, "GetProduct", CommandType.StoredProcedure);

            return result;
        }



        public async Task<FoodProductEntity?> GetSingleFood(int Id)
        {
            var parameters = new { Id= Id };

            FoodProductEntity? result = await _dapperConnection.Query< FoodProductEntity>(parameters, "GetSingleProduct", CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateFood(AddFoodProductDTO addFoodProductDTO, int Id, Guid userId)
        {
            var parameters = new { FoodId = Id, Name = addFoodProductDTO.Name, Price = addFoodProductDTO.Price, Image = addFoodProductDTO.Image, UserId= userId };

            await _dapperConnection.Execute(parameters, "UpdateProduct", CommandType.StoredProcedure);
        }
    }
}









//using var command = new SqlCommand("GetProduct", connection)
//{
//    CommandType = CommandType.StoredProcedure
//};

//await connection.OpenAsync();
//using var reader = await command.ExecuteReaderAsync();

//while (await reader.ReadAsync())
//{
//    foodEntities.Add(new FoodProductEntity()
//    {
//        Id = reader.GetInt32(0),
//        Name = reader.GetString(1),
//        Price = reader.GetDecimal(2),
//        Image = reader.GetString(3)
//    });
//}
