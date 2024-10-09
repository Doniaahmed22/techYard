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
    public class ProductRepository : IProductRepository
    {
        private readonly techYardDbContext _context;
        public ProductRepository(techYardDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Products entity)
        {
            await _context.products.AddAsync(entity);
        }

        public void Delete(Products entity)
        {
            _context.products.Remove(entity);
        }

        public async Task<IReadOnlyList<Products>> GetAllAsync()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int? id)
        {
            return await _context.products.FindAsync(id);
        }

        public void Update(Products entity)
        {
            _context.products.Update(entity);
        }
    }
}
