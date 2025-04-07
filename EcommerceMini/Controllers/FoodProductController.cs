using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.MiddleWare;
using BusinessLogicLayer.Services;
using DomainLayer.DTOs.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommerceMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodProductController 
        (
           IFoodProductService foodProductService
        )
        : ControllerBase
    {

        private readonly IFoodProductService _foodProductService= foodProductService;

        [HttpPost("add-food"), Authorize]
        [ServiceFilter(typeof(AdminJwtValidityMiddleWare))]
        public async Task<IActionResult> AddFood([FromBody] AddFoodProductDTO addFoodProductDTO)
        {

            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                await _foodProductService.AddFood(addFoodProductDTO, userId);

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
        [ServiceFilter(typeof(UserJwtValidityMiddleWare))]
        public async Task<IActionResult> GetFood()
        {
            try
            {
                var result = await _foodProductService.GetFood();

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
        [ServiceFilter(typeof(UserJwtValidityMiddleWare))]
        public async Task<IActionResult> GetSingleFood([FromQuery] int id)
        {
            try
            {
                var result = await _foodProductService.GetSingleFood(id);

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


        [HttpPut("update/{id:int}"), Authorize]
        [ServiceFilter(typeof(AdminJwtValidityMiddleWare))]
        public async Task<IActionResult> UpdateFood([FromBody] AddFoodProductDTO addFoodProductDTO, int id)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                await _foodProductService.UpdateFood(addFoodProductDTO, id, userId);

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
        [ServiceFilter(typeof(AdminJwtValidityMiddleWare))]
        public async Task<IActionResult> DeleteFood([FromQuery] int id)
        {
            Console.WriteLine("Here we are \n\n" + id);
            try
            {
                Guid userId = Guid.Parse(HttpContext.Items["UserId"].ToString());

                await _foodProductService.DeleteFood(id, userId);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
