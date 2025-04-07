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
    public class UserRoleRepository(
        IDapperConnection dapperConnection
        ): IUserRoleRepository
    {
        private readonly IDapperConnection _dapperConnection = dapperConnection;

        public async Task AddUserRole(UserRoleEntity userRoleEntity)
        {
            var parameters = new { RoleId = userRoleEntity.RoleId, UserId = userRoleEntity.UserId, UserRole = userRoleEntity.UserRole };

            await _dapperConnection.Execute(parameters, "AddUserRole", CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<UserRoleEntity>?> GetRole(Guid userId)
        {

            var parameters = new { UserId = userId };

            IEnumerable<UserRoleEntity>? result = await _dapperConnection.QueryAll< UserRoleEntity>(parameters, "GetUserRole", CommandType.StoredProcedure);

            return result;
        }

    }
}
