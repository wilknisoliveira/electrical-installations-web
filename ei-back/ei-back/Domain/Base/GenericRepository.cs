﻿using ei_back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ei_back.Domain.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        List<T> FindAll();
        T FindById(long id);
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
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
                _context.SaveChanges();
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

        public T FindById(long id)
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
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else return null;
        }

        public void Delete(long id)
        {
            var result = FindById(id);
            if (result != null)
            {
                try
                {
                    _dbSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _dbSet.Any(g => g.Id.Equals(id));
        }
    }
}