using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using techYard.Data.Context;
using techYard.Data.Entities;
using techYard.Repository.Interfaces;

namespace techYard.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly techYardDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(techYardDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityKey = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(entityKey))
            {
                // Directly instantiate the repository
                var repositoryInstance = new GenericRepository<TEntity>(_context, this); // Use direct instantiation

                _repositories.Add(entityKey, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[entityKey];
        }


    }
    }
