using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUserRepository
    {

        public Task AddUser(string email, string passwordHash);

        public Task<UserEntity> GetUser(Guid userId);
        public Task<UserEntity> GetUserByMail(string email);
        public Task UpdateUserToken(Guid userId, string refreshToken, DateTime refreshTokenExpiry);
    }
}
