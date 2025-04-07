using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DomainLayer.DTOs.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IPasswordHash passwordHash,
        IUserService userService
        ) : ControllerBase
    {


        private readonly IPasswordHash _passwordHash= passwordHash;
        private readonly IUserService _userService= userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpDto userSignUpDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode
                        (
                        StatusCodes.Status400BadRequest,
                        new { message = "Please check your request details" }
                        );
                }

                
                string passwordHash=  _passwordHash.HashPassword(userSignUpDto.Password);

                await _userService.AddUser(userSignUpDto.Email, passwordHash);

                return StatusCode(
                    StatusCodes.Status201Created,
                    new { message = "User was registered successfully" }
                    );

            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "Something went wrong, please try again later" }
                    );
            }
        }

    }
}
