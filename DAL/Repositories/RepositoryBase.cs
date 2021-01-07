using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        public T FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Insert(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }

    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll();

        public T FindById(Guid id);

        public void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
