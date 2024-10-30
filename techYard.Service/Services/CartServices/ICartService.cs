using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Service.Services.AccountServices.Dtos;
using techYard.Service.Services.CartServices.Dtos;
using techYard.Service.Services.CategoryServices.Dtos;

namespace techYard.Service.Services.CartServices
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(string userId);
        Task AddProductToCartAsync(string userId, int productId, int quantity);
        Task RemoveProductFromCartAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}
