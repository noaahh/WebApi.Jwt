using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Jwt.Models.Helper;

namespace WebApi.Jwt.Controllers
{
    [Authorize(Policy = ControllerPolicies.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        // GET api/protected
        [HttpGet]
        public IActionResult ProtectedRessource()
        {
            return new OkObjectResult(new {result = true});
        }
    }
}