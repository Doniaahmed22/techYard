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
        Task<IReadOnlyList<GetCart>> GetAllProductsInCart();
        Task<GetCart> GetProductInCartByCartId(int id);
        Task<GetCart> AddProductInCart(AddCart cartDto);
        Task<GetCart> DeleteProductFromCartById(int id);
        Task<GetCart> UpdateProductInCart(int id, AddCart entity);
        Task<GetCart> IsProductAndUserAlreadyExist(int productId, string userId);
        Task<IReadOnlyList<GetProductsInCart>> GetProductInCartByUserId(string UserId);
        Task<IReadOnlyList<GetAllProductsFromCart>> GetAllProductsFromTheCart();
        Task<IReadOnlyList<GetAllUsersFromCart>> GetAllUsersFromTheCart();
        Task<IReadOnlyList<UserProductsDto>> GetUsersWithProductsAsync();


    }
}
