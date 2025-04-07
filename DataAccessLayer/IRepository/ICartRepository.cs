using Dapper;
using DomainLayer.DTOs.RequestDTO;
using DomainLayer.DTOs.ResponseDTO;
using DomainLayer.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICartRepository
    {
        public Task AddCart(AddCartDTO addCartDTO, Guid userId, decimal totalPrice, int quantity);
        public Task<IEnumerable<CartResponseDto>> GetCart(Guid userId);

        public Task<CartResponseDto> GetSingleCart(int Id, Guid userId);

        public Task UpdateCart(int Id, Guid userId, decimal totalPrice, int quantity);

        public Task DeleteCart(int Id, Guid userId);

    }
}
