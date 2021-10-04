using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TreloDAL.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
            );
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true
            );
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void DeleteById(int id);
        void Remove(T item);
        List<T> ToList();
    }
}
