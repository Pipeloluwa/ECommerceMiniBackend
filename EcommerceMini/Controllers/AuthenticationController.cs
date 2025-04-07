using BusinessLogicLayer.IServices;
using DomainLayer.DTOs;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceMini.Controllers
{
    [Route("api/authentication/")]
    [ApiController]
    public class AuthenticationController
        (
        IUserService userService,
        IUserRoleService userRoleService,
        IJwtTokenService _jwtTokenService,
        IPasswordHash passwordHash
        ) : ControllerBase
    {

        private readonly IUserService _userService= userService;
        private readonly IUserRoleService _userRoleService= userRoleService;
        private readonly IJwtTokenService jwtTokenService = _jwtTokenService;
        private readonly IPasswordHash _passwordHash = passwordHash;



        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLogin)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Please check your request details" });
                }


                UserEntity? user = await _userService.GetUserByMail(userLogin.Email);

                if (user == null)
                {
                    return StatusCode
                        (
                            StatusCodes.Status404NotFound,
                            new { message = "This user could not be found or has been deactivated" }
                        );
                }


                bool verifyHash = _passwordHash.VerifyHashPassword(userLogin.Password, user.PasswordHash);

                if (!verifyHash)
                {
                    return Unauthorized("Please check your login details");
                }

                IEnumerable<UserRoleEntity>? userRole = await _userRoleService.GetRole(user.UserId);

                var jwtToken = await jwtTokenService.GenerateToken(user, userRole.FirstOrDefault().RoleId.ToString());

                var refreshToken = await jwtTokenService.GenerateRefreshToken();

                await _userService.UpdateUserToken(user.UserId, refreshToken, DateTime.UtcNow.AddDays(1));

                return Ok
                    (
                        new
                        {
                            message = "User logged in Successfully",
                            token = jwtToken,
                            refresh_token = refreshToken,
                            user = new { user?.Email },
                            userRole= new {userRole.FirstOrDefault().RoleId}
                        }
                    );

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong, please try again later");
            }
        }




        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Please check your request details" });
                }


                var principal = await jwtTokenService.GetJWTPrincipal(form.JWTToken);

                var principalUserID = (principal?.Claims.FirstOrDefault(c => c.Type == "UserId"))?.Value;

                if (principalUserID.IsNullOrEmpty())
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User details could not be extracted" });
                }


                var user = await _userService.GetUser(Guid.Parse(principalUserID));


                if (user == null)
                {
                    return StatusCode
                        (
                            StatusCodes.Status404NotFound,
                            new { message = "This user could not be found or has been deactivated" }
                        );
                }


                if (form.RefreshToken != user.RefreshToken || DateTime.UtcNow > user.RefreshTokenExpiry)
                {
                    return StatusCode
                        (
                            StatusCodes.Status400BadRequest,
                            new
                            {
                                message = "Please check if you provided the correct credentials"
                            }
                        );
                }

                IEnumerable<UserRoleEntity>? userRole = await _userRoleService.GetRole(user.UserId);

                var jwtToken = await jwtTokenService.GenerateToken(user, userRole.FirstOrDefault().RoleId.ToString());

                var refreshToken = await jwtTokenService.GenerateRefreshToken();

                await _userService.UpdateUserToken(user.UserId, refreshToken, DateTime.UtcNow.AddHours(24));

                return StatusCode
                    (
                        StatusCodes.Status200OK,
                        new
                        {
                            message = "Token was refreshed Successfully, New Token Generated!",
                            token = jwtToken,
                            refresh_token = refreshToken,
                        }
                    );

            }
            catch (Exception ex)
            {
                return StatusCode
                        (
                            StatusCodes.Status500InternalServerError,
                            new { message = "Could not complete request" }
                        );
            }

        }


    }
}
