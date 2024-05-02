using Microsoft.AspNetCore.Mvc;

namespace Backend_Api.Controllers
{
    public class TokenValidationController : Controller
    {
        [HttpGet]
        [Route("api/validatetoken")]
        public IActionResult ValidateToken([FromHeader] HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            string[] header_and_token = authHeader.Split(' ');
            string header = header_and_token[0];
            string token = header_and_token[1];
            if (header != "Bearer")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Token");
            }
            return StatusCode(StatusCodes.Status200OK, "Success");
        }
    }
}
