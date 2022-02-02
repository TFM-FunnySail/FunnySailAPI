using FunnySailAPI.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure
{
    public class DatabaseTransactionFactory : IDatabaseTransactionFactory
    {
        private ApplicationDbContext _dbContext;

        public DatabaseTransactionFactory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_dbContext);
        }
    }
}
