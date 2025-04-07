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
    public class CartRepository(
        IDapperConnection dapperConnection
        ): ICartRepository
    {
        private readonly IDapperConnection _dapperConnection = dapperConnection;

        public async Task AddCart(AddCartDTO addCartDTO, Guid userId, decimal totalPrice, int quantity)
        {
            var parameters = new { UserId= userId, FoodId= addCartDTO.FoodId, Quantity= quantity, TotalPrice= totalPrice };

            await _dapperConnection.Execute(parameters, "AddCart", CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<CartResponseDto>> GetCart(Guid userId)
        {
            var parameters = new { UserId = userId };

            IEnumerable<CartResponseDto> result = await _dapperConnection.QueryAll<CartResponseDto>(parameters, "GetCart", CommandType.StoredProcedure);

            return result;
        }


        public async Task<CartResponseDto> GetSingleCart(int Id, Guid userId)
        {
            var parameters = new { Id = Id, UserId= userId };

            CartResponseDto? result = await _dapperConnection.Query< CartResponseDto>(parameters, "GetSingleCart", CommandType.StoredProcedure);

            return result;
        }


        public async Task UpdateCart(int Id, Guid userId, decimal totalPrice, int quantity)
        {
            var parameters = new { Id = Id,  UserId = userId, Quantity = quantity, TotalPrice = totalPrice };

            await _dapperConnection.Execute(parameters, "UpdateCart", CommandType.StoredProcedure);
        }

        public async Task DeleteCart(int Id, Guid userId)
        {
            var parameters = new { CartId = Id, UserId = userId };

            await _dapperConnection.Execute(parameters, "DeleteCart", CommandType.StoredProcedure);
        }

    }
}
