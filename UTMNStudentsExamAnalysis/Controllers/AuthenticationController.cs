using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace UTMNStudentsExamAnalysis.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var result = _authenticationService.Login(model.Username, model.Password);
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(new { token = result });
        }

        // Add logout endpoint if needed
    }
}
