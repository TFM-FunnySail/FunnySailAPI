using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces
{
    public interface IDatabaseTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
        Task DisposeAsync();
    }
}
