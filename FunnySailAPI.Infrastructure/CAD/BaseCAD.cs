using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD
{
    public class BaseCAD<T> : IBaseCAD<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        public BaseCAD(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> FindById(int id)
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<T> FindById(string id)
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll(Pagination pagination)
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return await dbSet.Skip(pagination.First).Take(pagination.Size).ToListAsync();
        }

        public virtual async Task<int> GetCounter()
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return await dbSet.CountAsync();
        }

        public virtual async Task<T> AddAsync(T newEntity)
        {
            await _dbContext.AddAsync(newEntity);
            await _dbContext.SaveChangesAsync();

            return newEntity;
        }

        public virtual async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Any(IQueryable<T> query)
        {
            return await query.AnyAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            DbSet<T> dbSet = _dbContext.Set<T>();
            return dbSet.AsQueryable();
        }

        public async Task<List<T>> GetAll(IQueryable<T> query, Pagination pagination)
        {
            return await query.Skip(pagination.First).Take(pagination.Size).ToListAsync();
        }

        public virtual async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
