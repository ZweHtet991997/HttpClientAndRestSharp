using Backend_Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Api
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            string requestPath = context.Request.Path;
            if (requestPath == "api/login" || requestPath == "/weatherforecast")
            {
                await _next.Invoke(context);
            }
            else
            {
                if (authHeader != null && authHeader.StartsWith("Bearer"))
                {
                    string[] header_and_token = authHeader.Split(' ');
                    string header = header_and_token[0];
                    string token = header_and_token[1];
                    TokenValidationController tokenValidationController = new TokenValidationController();
                    ObjectResult objectResult = (ObjectResult)tokenValidationController.ValidateToken(context);
                    if (objectResult.StatusCode == 200)
                    {
                        if (requestPath == "/api/validatetoken")
                        {
                            context.Response.StatusCode = 200;
                            return;
                        }
                        else
                        {
                            await _next.Invoke(context);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                }
                //if authHeader null or not Bearer
                else
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
        }
    }
}
