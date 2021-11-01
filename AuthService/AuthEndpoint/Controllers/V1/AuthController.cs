using Application.Services;
using AuthEndpoint.APIRespond;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthController : Controller
    {
        private readonly ITokenGenerationService _tokenGenerationService;

        public AuthController(ITokenGenerationService tokenGenerationService)
        {
            _tokenGenerationService = tokenGenerationService;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateAsync([FromBody] UserLoginViewModel loginModel)
        {
            var result = await _tokenGenerationService.ValidateUserAndGetJWT(loginModel);
            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().FailedToFind("username And password does not match. please try again");
            }
        }
    }
}
