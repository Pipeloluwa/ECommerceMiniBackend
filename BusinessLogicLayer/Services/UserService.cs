using BusinessLogicLayer.IServices;
using Dapper;
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
    public class UserService(
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository
        ) : IUserService
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;


        public async Task AddUser(string email, string passwordHash)
        {
            try
            {
                await _userRepository.AddUser(email, passwordHash);
                UserEntity? user= await _userRepository.GetUserByMail(email);
   
                UserRoleEntity userRoleEntity = new()
                {
                    RoleId= 2,
                    UserId = user.UserId,
                    UserRole = "user"
                };

                await _userRoleRepository.AddUserRole(userRoleEntity);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Could not create");
            }

        }

        public async Task<UserEntity> GetUser(Guid userId)
        {
            try
            {
                return await _userRepository.GetUser(userId);
            }
            catch (Exception)
            {

                throw new HttpRequestException("Could not create");
            }
        }

        public async Task<UserEntity> GetUserByMail(string email)
        {
            try
            {
                return await _userRepository.GetUserByMail(email);
            }
            catch (Exception)
            {

                throw new HttpRequestException("Could not retrieve user");
            }
        }


        public async Task UpdateUserToken(Guid userId, string refreshToken, DateTime refreshTokenExpiry)
        {

            try
            {
                await _userRepository.UpdateUserToken(userId, refreshToken, refreshTokenExpiry);
            }
            catch (Exception)
            {

                throw new HttpRequestException("Could not update token and token expiry");
            }

        }

    }
}
