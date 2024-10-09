using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository <TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
