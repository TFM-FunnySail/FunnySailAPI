using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces
{
    public interface IDatabaseTransactionFactory
    {
        IDatabaseTransaction BeginTransaction();
    }
}
