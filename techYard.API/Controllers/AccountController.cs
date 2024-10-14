using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using techYard.Service.Services.AccountServices;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;
        public AccountController(IAccountService accountService )
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid model"
                });
            }

            try
            {
                var result = await _accountService.RegisterCustomer(model);

                if (result.Succeeded)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        Data = model // Adjust if necessary
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "User registration failed.",
                    Data = result.Errors.Select(e => e.Description).ToArray()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Invalid model"
                });
            }

            var result = await _accountService.Login(model);

            if (result.IsSuccess)
            {
                var user = await _accountService.GetUserFromToken(result.Token);
                var authDto = _mapper.Map<AuthDTO>(user);
                authDto.Token = result.Token;
                authDto.ProfileImage = await _accountService.GetUserProfileImage(user.ProfileId);

                return Ok(new BaseResponse
                {
                    status = true,
                    Data = authDto
                });
            }

            return Unauthorized(new BaseResponse
            {
                status = false,
                ErrorCode = 401,
                ErrorMessage = result.ErrorMessage
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var user = await _accountService.GetUserFromToken(token);
                var isSuccess = await _accountService.Logout(user);

                if (isSuccess)
                {
                    return Ok(new BaseResponse
                    {
                        status = true,
                        Data = "Successfully logged out"
                    });
                }

                return BadRequest(new BaseResponse
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Logout failed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }
    }
}
