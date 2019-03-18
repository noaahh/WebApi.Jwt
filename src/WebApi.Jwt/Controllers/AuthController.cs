using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Jwt.Core.Domain.Entities.Requests;
using WebApi.Jwt.Core.Interfaces.Services;
using WebApi.Jwt.Models.Helper;

namespace WebApi.Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AuthSettings _authSettings;

        public AuthController(IOptions<AuthSettings> authSettings, IAuthService authService)
        {
            _authSettings = authSettings.Value;
            _authService = authService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _authService.LoginAsync(new LoginRequest(request.UserName, request.Password,
                Request.HttpContext.Connection.RemoteIpAddress?.ToString()));

            return Ok(response);
        }

        // POST api/auth/refreshtoken
        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] ExchangeRefreshTokenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _authService.ExchangeRefreshTokenAsync(
                new ExchangeRefreshTokenRequest(request.AccessToken, request.RefreshToken, _authSettings.SecretKey));

            return Ok(response);
        }
    }
}