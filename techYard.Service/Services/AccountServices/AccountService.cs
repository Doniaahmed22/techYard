using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using techYard.Service.Services.AccountServices.Dtos;

namespace techYard.Service.Services.AccountServices
{
    public class AccountService : IAccountService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IMapper mapper;


        public AccountService( UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<JWT> jwt, IMapper _mapper, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            mapper = _mapper;
            _signInManager = signInManager;

        }
        //------------------------------------------------------------------------------------------------------------
        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id && x.Status);
            return user;
        }
        //------------------------------------------------------------------------------------------------------------
        // Check if email or phone number already exists before creating or updating the user
        private async Task<bool> IsEmailExistAsync(string email, string userId = null)
        {
            return await _userManager.Users.AnyAsync(u =>
                (u.Email == email) && u.Id != userId);
        }

        // Helper methods for handling profile images


        // Register methods
        public async Task<IdentityResult> RegisterCustomer(RegisterCustomer model)
        {
            if(await IsEmailExistAsync(model.Email) == true)
            {
                throw new InvalidOperationException("This Email Already Exist Try Another One");
            }
            var user = mapper.Map<ApplicationUser>(model);
            user.ProfileImagePath = "Images/Profile/Profile.jpeg";
            user.PhoneNumberConfirmed = true;
            user.EmailConfirmed = true;
            user.UserName = model.PhoneNumber;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                // Handle potential errors by throwing an exception or logging details
                throw new InvalidOperationException("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return result;
        }

        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(Login model)
        {
            try
            {
                bool checkEmail;
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(model.PhoneNumberOrEmail);
                    checkEmail = true;
                }
                catch
                {
                    checkEmail = false;
                }
                var user = (checkEmail) ? await _userManager.FindByEmailAsync(model.PhoneNumberOrEmail) : await _userManager.FindByNameAsync(model.PhoneNumberOrEmail);
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
                var role = await _userManager.GetRolesAsync(user);
                var token = await GenerateJwtToken(user, role.First());
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

        //------------------------------------------------------------------------------------------------------------

        public Task<List<string>> GetRoles()
        {
            return _roleManager.Roles.Select(x => x.Name).ToListAsync();
        }

        //------------------------------------------------------------------------------------------------------------
        public async Task<IdentityResult> Activate(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("Admin not found");

            user.Status = true;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> Suspend(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("Admin not found");

            user.Status = false;
            return await _userManager.UpdateAsync(user);
        }

        //------------------------------------------------------------------------------------------------------------
        #region create and validate JWT token

        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("name", user.FullName),
                new Claim(ClaimTypes.Role, Role),
            };

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


        public async Task<ApplicationUser> GetUserFromToken(string token)
        {
            try
            {
                var userId = ValidateJwtToken(token);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        Task<bool> IAccountService.IsPhoneExistAsync(string phoneNumber, string userId)
        {
            throw new NotImplementedException();
        }

        Task<JwtSecurityToken> IAccountService.GenerateJwtToken(ApplicationUser user, string Role)
        {
            throw new NotImplementedException();
        }

        #endregion Random number and string
    }
}



