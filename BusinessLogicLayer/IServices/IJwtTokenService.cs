using DomainLayer.Entities;
using System.Security.Claims;

namespace BusinessLogicLayer.IServices
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(UserEntity userEntity, string roleId);
        Task<ClaimsPrincipal?> GetJWTPrincipal(string token);
        Task<string> GenerateRefreshToken();

    }
}
