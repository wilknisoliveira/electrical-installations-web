using ei_back.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace ei_back.Infrastructure.Context
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
        void Rollback();
        IRepository<T> GetRepository<T>() where T : BaseEntity;
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresContext _context;
        private Dictionary<Type, Object> _repositories;

        public UnitOfWork(PostgresContext dbContext)
        {
            _context = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //TODO: implement rollback
            throw new NotImplementedException();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
