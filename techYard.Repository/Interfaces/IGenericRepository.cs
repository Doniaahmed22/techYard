using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //Task<TEntity> GetByIdAsync(int? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);

        //Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity entity);
        Task Update(TEntity new_entity);
        Task<TEntity> Delete(int id);
    }
}
