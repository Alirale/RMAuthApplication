using InfoEndpoint.APIRespond;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    public class UserController : Controller
    {
        private readonly IUserInfosRepository _userInfosRepository;

        public UserController(IUserInfosRepository userInfosRepository)
        {
            _userInfosRepository = userInfosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userInfosRepository.GetAllPeapleInfo();
            if (result != null)
            {
                return new ApiResponse().Success(result);
            }
            else
            {
                return new ApiResponse().Failed("Something Went Wrong");
            }
        }
    }
}
