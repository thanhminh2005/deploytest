using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DataContext dataContext)
        {
            Context = dataContext;
            _dbSet = Context.Set<T>();
        }

        public DbContext Context { get; }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }

    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll();

        public Task<T> FindByIdAsync(Guid id);

        public Task InsertAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
