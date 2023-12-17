using ei_back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ei_back.Domain.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        List<T> FindAll();
        T FindById(Guid id);
        T Update(T item);
        void Delete(Guid id);
        bool Exists(Guid id);
    }


    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected PostgresContext _context;

        //Pass the dataset dinamically
        private DbSet<T> _dbSet;

        public GenericRepository(PostgresContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                _context.Add(item);
                return item;
            }
            catch
            {
                throw;
            }
        }

        public List<T> FindAll()
        {
            return _dbSet.ToList();
        }

        public T FindById(Guid id)
        {
            return _dbSet.SingleOrDefault(g => g.Id.Equals(id));
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = FindById(item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else return null;
        }

        public void Delete(Guid id)
        {
            var result = FindById(id);
            if (result != null)
            {
                try
                {
                    _dbSet.Remove(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(Guid id)
        {
            return _dbSet.Any(g => g.Id.Equals(id));
        }
    }
}
