using BussinessLogic.Interfaces;
using BussinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Education.Classes.Item.Assignments.Item.Submissions.Item.Return;

namespace WER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginVM userInfo)
        {
            try
            {
                UserSessionVM session = await _userService.LoginUser(userInfo);
                if (session == null)
                {
                    return Unauthorized("Invalid Credentials");
                }
                return Ok(session);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterVM userInfo)
        {
            try
            { 
                return Ok(await _userService.RegisterUser(userInfo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
