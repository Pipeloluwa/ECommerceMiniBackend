using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DomainLayer.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserRoleService(
            IUserRoleRepository userRoleRepository
        ): IUserRoleService
    {

        private readonly IUserRoleRepository _userRoleRepository= userRoleRepository;


        public async Task<IEnumerable<UserRoleEntity>?> GetRole(Guid userId)
        {
            return await _userRoleRepository.GetRole(userId);
        }
    }
}
