using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreloDAL.Data;
using TreloDAL.Repository.IRepository;

namespace TreloDAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TreloDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(TreloDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void DeleteById(int id)
        {
            var item = dbSet.Find(id);
            dbSet.Remove(item);
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedEnumerable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                query = (IQueryable<T>)orderBy(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.ToList();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void Update(T item)
        {
            dbSet.Update(item);
        }

        public List<T> ToList()
        {
            return dbSet.ToList();
        }
    }
}
