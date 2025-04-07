using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace BusinessLogicLayer.MiddleWare
{
    public class UserJwtValidityMiddleWare: IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync (ActionExecutingContext httpContext, ActionExecutionDelegate next)
        {
            var context = httpContext.HttpContext;

            using var scope = context.RequestServices.CreateScope();
            var jwtTokenService = scope.ServiceProvider.GetService<IJwtTokenService>();

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();



            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("This user credential could not be found");
                context.Response.ContentType = "application/json";

                // short-circuiting the pipeline
                return;
            }

            var principal = await jwtTokenService.GetJWTPrincipal(token);

            var principalUserID = (principal?.Claims.FirstOrDefault(c => c.Type == "UserId"))?.Value;


            if (string.IsNullOrEmpty(principalUserID))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("This user credential could not be found");
                context.Response.ContentType = "application/json";

                // short-circuiting the pipeline
                return;
            }

            context.Items["UserId"] = principalUserID;
            context.Items["JwtToken"] = token;


            await next();
        }

    }
}
