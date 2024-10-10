using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity new_entity)
        {
            _context.Set<TEntity>().Update(new_entity);
            //await _unitOfWork.CompleteAsync();

        }
    }
}
