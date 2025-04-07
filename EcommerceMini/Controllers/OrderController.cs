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
    public class OrderController(
            IOrderService orderService
        ) : ControllerBase
    {


        private readonly IOrderService _orderService= orderService;

        [HttpPost("add-order")]
        [ServiceFilter(typeof(UserJwtValidityMiddleWare))]
        public async Task<IActionResult> AddOrder([FromBody] IEnumerable<AddOrderDTO> addOrderDTOs)
        {

            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                await _orderService.AddOrder(userId, addOrderDTOs);

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
        [ServiceFilter(typeof(AdminJwtValidityMiddleWare))]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var result = await _orderService.GetOrder(userId);

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

    }
}
