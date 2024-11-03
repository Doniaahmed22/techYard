using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Context;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;


namespace techYard.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly techYardDbContext _context;
        readonly IUnitOfWork _unitOfWork;
        public GenericRepository(techYardDbContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                //await _unitOfWork.CompleteAsync();
                return entity;

            }
            return null;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // تطبيق Include على كل علاقة يتم تمريرها
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            // بدء الاستعلام عن الكيان بناءً على المعرف
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // تطبيق Include على كل علاقة يتم تمريرها
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // استخدام FirstOrDefaultAsync للبحث عن الكيان بناءً على المعرف
            return await query.FirstOrDefaultAsync(e => e.Id == id); // تأكد من أن TEntity يحتوي على خاصية Id
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task Update(TEntity new_entity)
        {
            _context.Set<TEntity>().Update(new_entity);
            //await _unitOfWork.CompleteAsync();

        }


        public async Task<ProductsInCart> IsProductAndUserAlreadyExist(int productId, string userId)
        {
            var productInCart = await _context.Set<ProductsInCart>()
            .FirstOrDefaultAsync(p => p.ProductId == productId);

            return productInCart;
        }



        public async Task<IReadOnlyList<ProductsInCart>> GetCartsByUserId(string userId)
        {
            var productsInCart = await _context.Set<ProductsInCart>().Include(p=> p.Product).ToListAsync();

            return productsInCart;
        }



        public async Task<IReadOnlyList<ProductsInCart>> GetAllProductsFromTheCart()
        {
            var productsInCart = await _context.Set<ProductsInCart>().Include(p=> p.Product).ToListAsync();

            return productsInCart;
        }


        public async Task<IReadOnlyList<ProductsInCart>> GetAllUsersFromTheCart()
        {
            var UsersInCart = await _context.Set<ProductsInCart>().ToListAsync();

            return UsersInCart;
        }










        public async Task<IReadOnlyList<ProductsInCart>> GetAllCartsAsync()
        {
            return await _context.Set<ProductsInCart>()
                .Include(pc => pc.Product) // تأكد من تضمين المنتج
                .ToListAsync();
        }




    }
}
