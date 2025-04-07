using Dapper;
using DataAccessLayer.Dapper;
using DataAccessLayer.IRepository;
using DomainLayer.DTOs.RequestDTO;
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
    public class UserRepository(
        IConfiguration configuration,
        IDapperConnection dapperConnection
        ): IUserRepository
    {

        private readonly IDapperConnection _dapperConnection = dapperConnection;


        public async Task AddUser(string email, string passwordHash)
        {
            var parameters = new { Email = email, PasswordHash = passwordHash };

            await _dapperConnection.Execute(parameters, "CreateUser", CommandType.StoredProcedure);
        }

        public async Task<UserEntity?> GetUser(Guid userId)
        {

            var parameters = new { UserId = userId };

            UserEntity? result= await _dapperConnection.Query<UserEntity>(parameters, "GetUser", CommandType.StoredProcedure);

            return result;
        }

        public async Task<UserEntity?> GetUserByMail(string email)
        {

            var parameters = new { Email = email };

            UserEntity? result = await _dapperConnection.Query<UserEntity?>(parameters, "GetUserByMail", CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateUserToken(Guid userId, string refreshToken, DateTime refreshTokenExpiry)
        {

            var parameters = new { UserId = userId, RefreshToken= refreshToken, RefreshTokenExpiry= refreshTokenExpiry };

            await _dapperConnection.Execute(parameters, "UpdateUserToken", CommandType.StoredProcedure);
        }


    }
}
