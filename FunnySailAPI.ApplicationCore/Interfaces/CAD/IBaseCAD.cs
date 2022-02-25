using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD
{
    public interface IBaseCAD<T> where T : class
    {
        Task<List<T>> GetAll(Pagination pagination);
        Task<int> GetCounter();
        Task<T> FindById(int id);
        Task<T> AddAsync(T newEntity);
        Task<bool> Any(IQueryable<T> query);
        IQueryable<T> GetIQueryable();
        Task<List<T>> GetAll(IQueryable<T> query,Pagination pagination);
        Task<IList<T>> Get(IQueryable<T> query=null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", Pagination pagination = null);
        Task<T> Update(T entity);
        Task<T> FindById(string id);
        Task Delete(T entity);
    }
}
