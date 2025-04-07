using BusinessLogicLayer.IServices;
using BusinessLogicLayer.MiddleWare;
using DomainLayer.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    [ServiceFilter(typeof(UserJwtValidityMiddleWare))]
    public class CartController(
            ICartService cartService
        ) : ControllerBase
    {

        private readonly ICartService _cartService = cartService;




        [HttpPost("add-cart")]
        public async Task<IActionResult> AddCart([FromBody] AddCartDTO addCartDTO)
        {

            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                await _cartService.AddCart(addCartDTO, userId);

                return StatusCode
                    (
                        StatusCodes.Status201Created,
                        new { message = "Created Successfully" }
                    );
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong, please try again later");
            }
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var result = await _cartService.GetCart(userId);

                return StatusCode
                    (
                        StatusCodes.Status200OK,
                        new { message = "Gotten Successfully", data = result }
                    );
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong, please try again later");
            }
        }



        [HttpGet("get-single")]
        public async Task<IActionResult> GetSingleCart([FromQuery] int id)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var result = await _cartService.GetSingleCart(id, userId);

                return StatusCode
                    (
                        StatusCodes.Status200OK,
                        new { message = "Gotten Successfully", data = result }
                    );
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong, please try again later");
            }
        }


        [HttpPut("update"), Authorize]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartDTO updateCartDTO)
        {
            try
            {
                if (updateCartDTO.Quantity < 1)
                {
                    return StatusCode
                         (
                            StatusCodes.Status400BadRequest,
                            new { message = "Product quantity can not be less than 1" }
                        );
                }

                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                await _cartService.UpdateCart(updateCartDTO, userId);

                return StatusCode
                    (
                        StatusCodes.Status200OK,
                        new { message = "Updated Successfully" }
                    );
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong, please try again later");
            }
        }



        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCart([FromBody] IEnumerable<int> id)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                await _cartService.DeleteCart(id, userId);

                return StatusCode
                    (
                        StatusCodes.Status204NoContent,
                        new { message = "Deleted Successfully" }
                    );
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
