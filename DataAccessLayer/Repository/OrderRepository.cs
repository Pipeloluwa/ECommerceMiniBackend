using Dapper;
using DataAccessLayer.Dapper;
using DataAccessLayer.IRepository;
using DomainLayer.DTOs.RequestDTO;
using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class OrderRepository(
        IDapperConnection dapperConnection
    ): IOrderRepository
    {

        private readonly IDapperConnection _dapperConnection = dapperConnection;

        public async Task AddOrder(Guid userId, int foodId, int quantity, decimal totalPrice)
        {
            var parameters = new { UserId= userId, FoodId= foodId, Quantity= quantity, TotalPrice= totalPrice };

            await _dapperConnection.Execute(parameters, "AddOrder", CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<CartResponseDto>> GetOrder(Guid userId)
        {
            var parameters = new { UserId = userId };

            IEnumerable<CartResponseDto> result = await _dapperConnection.QueryAll<CartResponseDto>(parameters, "GetOrders", CommandType.StoredProcedure);

            return result;
        }


    }
}
