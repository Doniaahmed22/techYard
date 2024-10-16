using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using techYard.Data.Entities;
using techYard.Service.Services.AccountServices;
using techYard.Service.Services.AccountServices.Dtos;

namespace techYard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;
        readonly IMapper _mapper;
        readonly RoleManager<ApplicationRole> _roleManager;
        readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IAccountService accountService , IMapper mapper , RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest( "Invalid model");
            }

            try
            {
                var result = await _accountService.RegisterCustomer(model);

                if (result.Succeeded)
                {
                    return Ok(new 
                    {
                        status = true,
                        Data = model // Adjust if necessary
                    });
                }

                return BadRequest(new 
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "User registration failed.",
                    Data = result.Errors.Select(e => e.Description).ToArray()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new 
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
                var role = await _userManager.GetRolesAsync(user);
                var authDto = _mapper.Map<AuthDTO>(user);
                authDto.Token = result.Token;
                authDto.Role = role.First();
                authDto.ProfileImage = user.ProfileImagePath;


                return Ok(new
                {
                    status = true,
                    Data = authDto
                });
            }

            return Unauthorized(new 
            {
                status = false,
                ErrorCode = 401,
                ErrorMessage = result.ErrorMessage
            });
        }





        [HttpPost("UpdateProfileImage")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileImage([FromForm] ProfileImage profileImageDto)
        {
            // تحقق مما إذا كانت الصورة مرفقة
            if (profileImageDto.NewImage == null || profileImageDto.NewImage.Length == 0)
            {
                return BadRequest("No image file was provided.");
            }

            // الحصول على UserID من Claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in token.");
            }

            // العثور على المستخدم باستخدام UserManager
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // تحديد مسار حفظ الصورة الجديدة
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Profile");

            // إنشاء اسم فريد للصورة الجديدة
            var uniqueFileName = $"{user.Id}_{Guid.NewGuid()}_{profileImageDto.NewImage.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // حفظ الصورة في المسار المحدد
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileImageDto.NewImage.CopyToAsync(fileStream);
            }

            // تحديث مسار الصورة في قاعدة البيانات
            user.ProfileImagePath = $"Images/Profile/{uniqueFileName}";
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Profile image updated successfully!" });
        }












        [HttpPost("logout")]
        [Authorize]

        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var user = await _accountService.GetUserFromToken(token);
                var isSuccess = await _accountService.Logout(user);

                if (isSuccess)
                {
                    return Ok(new 
                    {
                        status = true,
                        Data = "Successfully logged out"
                    });
                }

                return BadRequest(new 
                {
                    status = false,
                    ErrorCode = 400,
                    ErrorMessage = "Logout failed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                {
                    status = false,
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> Create(RoleDTO roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = _mapper.Map<ApplicationRole>(roleModel);
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(roleModel);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, string.Join("; ", result.Errors.Select(e => e.Description)));
                    }
                }
                return Ok(roleModel);
            }
            catch (Exception ex)
            {
                var errorViewModel = new 
                {
                    Message = "خطا في حقظ البيانات",
                    StackTrace = ex.StackTrace
                };
                return BadRequest(errorViewModel);
            }
        }
    }
}
