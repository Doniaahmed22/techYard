using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;

namespace techYard.Service.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHandling _fileHandling;
        private readonly Jwt _jwt;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IHttpClientFactory clientFactory, UserManager<ApplicationUser> userManager, IFileHandling photoHandling,
         RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork,
         IOptions<Jwt> jwt, IMemoryCache _memoryCache, IMapper _mapper, SignInManager<ApplicationUser> signInManager, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
            _fileHandling = photoHandling;
            memoryCache = _memoryCache;
            mapper = _mapper;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IdentityResult> RegisterCustomer(RegisterCustomer model)
        {
            if (await IsPhoneExistAsync(model.PhoneNumber))
            {
                throw new ArgumentException("phone number already exists.");
            }

            var user = mapper.Map<ApplicationUser>(model);
            await SetProfileImage(user, model.ImageProfile);
            user.PhoneNumberConfirmed = true;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }

        public async Task<IdentityResult> RegisterAdmin(RegisterAdmin model)
        {
            if (await IsPhoneExistAsync(model.PhoneNumber))
            {
                throw new ArgumentException("phone number already exists.");
            }

            var user = mapper.Map<ApplicationUser>(model);
            await SetProfileImage(user, model.ImageProfile);
            user.PhoneNumberConfirmed = true;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }







        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return (false, null, "Invalid login attempt.");
                }

                // Check if user status is true and phone number is confirmed
                if (!user.Status)
                {
                    return (false, null, "Your account is deactivated. Please contact support.");
                }

                if (!user.PhoneNumberConfirmed)
                {
                    return (false, null, "Phone number not confirmed. Please verify your phone number.");
                }

                // Proceed with login
                await _signInManager.SignInAsync(user, model.RememberMe);
                var token = await GenerateJwtToken(user);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return (true, tokenString, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<bool> Logout(ApplicationUser user)
        {
            if (user == null)
                return false;

            await _signInManager.SignOutAsync();
            return true;
        }








        #region create and validate JWT token

        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user,string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("name", user.FullName),
                new Claim(ClaimTypes.Role, role),
            };
,
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_jwt.DurationInDays)),
                signingCredentials: creds);
            return token;
        }

        public string ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                if (token == null)
                    return null;
                if (token.StartsWith("Bearer "))
                    token = token.Replace("Bearer ", "");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "uid").Value;

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        #endregion create and validate JWT token

        #region Random number and string

        //Generate RandomNo
        public int GenerateRandomNo()
        {
            const int min = 1000;
            const int max = 9999;
            var rdm = new Random();
            return rdm.Next(min, max);
        }

        public string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomOTP(int length)
        {
            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion Random number and string
    }
}
