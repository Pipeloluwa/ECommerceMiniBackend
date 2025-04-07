using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class JwtTokenService(
        IUserRoleRepository userRoleRepository, 
        IConfiguration configuration
        ) : IJwtTokenService
    {
        private readonly IUserRoleRepository _userRoleRepository= userRoleRepository;
        private readonly IConfiguration _configuration = configuration;




        public async Task<string> GenerateToken(UserEntity userEntity, string roleId)
        {
            IEnumerable<UserRoleEntity>? userRole = await _userRoleRepository.GetRole(userEntity.UserId);

            var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", userEntity.UserId.ToString()),
                new Claim("Email", userEntity.Email.ToString()),
                new Claim("RoleId", roleId),
            };

            //ADDING ROLE CLAIMS
            claims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role.UserRole)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;

        }





        public async Task<ClaimsPrincipal?> GetJWTPrincipal(string token)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var validation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };


            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);

        }




        public async Task<string> GenerateRefreshToken()
        {
            var randomNo = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNo);
            }

            return Convert.ToBase64String(randomNo);

        }

    }
}
