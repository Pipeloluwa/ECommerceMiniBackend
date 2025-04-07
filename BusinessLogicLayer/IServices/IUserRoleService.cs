using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IUserRoleService
    {
        public Task<IEnumerable<UserRoleEntity>?> GetRole(Guid userId);
    }
}
