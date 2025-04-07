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
    public interface IUserRoleRepository
    {

        public Task AddUserRole(UserRoleEntity userRoleEntity);

        public Task<IEnumerable<UserRoleEntity>?> GetRole(Guid userId);

    }
}
