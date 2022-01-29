using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
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
    }
}
