using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Entities;

namespace techYard.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Products> GetByIdAsync(int? id);
        Task<IReadOnlyList<Products>> GetAllAsync();
        Task AddAsync(Products entity);
        void Update(Products entity);
        void Delete(Products entity);
    }
}
