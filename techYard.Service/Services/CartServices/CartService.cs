//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using techYard.Data.Context;
//using techYard.Data.Entities;
//using techYard.Repository.Interfaces;
//using techYard.Service.Services.CartServices.Dtos;
//using techYard.Service.Services.featuresServices.Dtos;

//namespace techYard.Service.Services.CartServices
//{
//    public class CartService : ICartService
//    {
//        readonly IUnitOfWork _unitOfWork;
//        readonly IMapper _mapper;
//        readonly techYardDbContext _context;
//        public CartService (IUnitOfWork unitOfWork, IMapper mapper, techYardDbContext context)
//        {
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//            _context = context;
//        }


//        public async Task<GetCart> AddProductInCart(AddCart cartDto)
//        {
//            // تحويل البيانات من AddCart إلى ProductsInCart باستخدام AutoMapper
//            var productInCart = _mapper.Map<ProductsInCart>(cartDto);

//            // إضافة المنتج إلى قاعدة البيانات
//            await _unitOfWork.Repository<ProductsInCart>().AddAsync(productInCart);
//            await _unitOfWork.CompleteAsync();

//            // إنشاء GetCart وإرجاعه
//            var getCart = _mapper.Map<GetCart>(productInCart);
//            return getCart;
//        }


//        public async Task<GetCart> DeleteProductFromCartById(int id)
//        {
//            var product = await _unitOfWork.Repository<ProductsInCart>().GetByIdAsync(id);
//            if (product == null)
//            {
//                return null;
//            }
//            await _unitOfWork.Repository<ProductsInCart>().Delete(id);
//            await _unitOfWork.CompleteAsync();
//            return new GetCart {
                
//            };
//        }

//        public async Task<IReadOnlyList<GetCart>> GetAllProductsInCart()
//        {
//            var products = await _unitOfWork.Repository<ProductsInCart>().GetAllAsync();
//            return _mapper.Map<IReadOnlyList<GetCart>>(products);
//        }

//        public async Task<GetCart> GetProductInCartByCartId(int id)
//        {
//            var product = await _unitOfWork.Repository<ProductsInCart>().GetByIdAsync(id);
//            if (product == null)
//            {
//                return null;
//            }
//            return _mapper.Map<GetCart>(product);

//        }

//        public async Task<GetCart> UpdateProductInCart(int id, AddCart cartDto)
//        {
//            var existingProduct = await _unitOfWork.Repository<ProductsInCart>().GetByIdAsync(id);
//            if (existingProduct == null)
//            {
//                return null;
//            }
//            _mapper.Map(cartDto, existingProduct);
//            await _unitOfWork.Repository<ProductsInCart>().Update(existingProduct);
//            await _unitOfWork.CompleteAsync();
//            return new GetCart { };
//        }


//        public async Task<GetCart> IsProductAndUserAlreadyExist(int productId, string userId)
//        {
//             var product = await _unitOfWork.Repository<ProductsInCart>().IsProductAndUserAlreadyExist(productId,userId);
//            var prod = new GetCart
//            {
//                Id = product.Id,
//                ProductId = product.ProductId,
//                Quantity = product.Quantity,
//            };
//            return prod;

//        }


//        public async Task<IReadOnlyList<GetProductsInCart>> GetProductInCartByUserId(string UserId)
//        {
//            var products = await _unitOfWork.Repository<ProductsInCart>().GetCartsByUserId(UserId);
//            if (products == null)
//            {
//                return null;
//            }
//            return _mapper.Map<IReadOnlyList<GetProductsInCart>>(products);

//        }


//        public async Task<IReadOnlyList<GetAllProductsFromCart>> GetAllProductsFromTheCart()
//        {
//           var products = await _unitOfWork.Repository<ProductsInCart>().GetAllProductsFromTheCart();
//            if (products == null)
//            {
//                return null;
//            }
//            return _mapper.Map<IReadOnlyList<GetAllProductsFromCart>>(products);

//        }


//        public async Task<IReadOnlyList<GetAllUsersFromCart>> GetAllUsersFromTheCart()
//        {
//            var usres = await _unitOfWork.Repository<ProductsInCart>().GetAllUsersFromTheCart();
//            if (usres == null)
//            {
//                return null;
//            }
//            return _mapper.Map<IReadOnlyList<GetAllUsersFromCart>>(usres);

//        }







//        public async Task<IReadOnlyList<UserProductsDto>> GetUsersWithProductsAsync()
//        {
//            var productsInCarts = await _unitOfWork.Repository<ProductsInCart>().GetAllCartsAsync(); // جلب جميع المنتجات في السلة

//            // تجميع البيانات إلى UserProductsDto
//            var userProducts = new List<UserProductsDto>();

//            return userProducts;
//        }


//    }
//}
