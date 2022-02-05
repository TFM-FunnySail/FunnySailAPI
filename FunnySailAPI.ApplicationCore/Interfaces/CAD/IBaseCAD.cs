using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD
{
    public interface IBaseCAD<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAll(Pagination pagination);
        Task<int> GetCounter();
        Task<T> FindById(int id);
        Task<T> AddAsync(T newEntity);
        Task<bool> Any(IQueryable<T> query);
        IQueryable<T> GetIQueryable();
        Task<IReadOnlyCollection<T>> GetAll(IQueryable<T> query,Pagination pagination);
        Task<T> Update(T entity);
    }
}
