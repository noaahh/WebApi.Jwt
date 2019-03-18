using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Jwt.Core.Domain.Entities.Requests;
using WebApi.Jwt.Core.Interfaces.Services;
using WebApi.Jwt.Models.Helper;

namespace WebApi.Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _accountService.RegisterUser(new RegisterUserRequest(request.FirstName,
                request.LastName, request.Email, request.UserName, request.Password));

            return Ok(response);
        }

        // GET api/protected/users
        [HttpGet("users")]
        [Authorize(Policy = ControllerPolicies.Admin)]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _accountService.GetUsers());
        }
    }
}