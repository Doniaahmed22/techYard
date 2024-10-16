using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;
using techYard.Service.Services.AccountServices.Dtos;

namespace techYard.Service.Services.AccountServices
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetUserById(string id);

        Task<bool> IsPhoneExistAsync(string phoneNumber, string userId = null);

        Task<IdentityResult> RegisterCustomer(RegisterCustomer model);

        Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(Login model);

        Task<bool> Logout(ApplicationUser user);
        Task<List<string>> GetRoles();

        Task<IdentityResult> Activate(string userId);

        Task<IdentityResult> Suspend(string userId);

        Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user, string Role);

        string ValidateJwtToken(string token);

        //Generate RandomNo
        int GenerateRandomNo();

        string RandomString(int length);

        string RandomOTP(int length);

        Task<ApplicationUser> GetUserFromToken(string token);


    }
}
