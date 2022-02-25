using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class RefundCP : IRefundCP
    {
        private readonly IRefundCEN _refundCEN;
        private IDatabaseTransactionFactory _databaseTransactionFactory;

        public RefundCP(IRefundCEN refundCEN,
                        IDatabaseTransactionFactory databaseTransactionFactory)
        {
            _refundCEN = refundCEN;
            _databaseTransactionFactory = databaseTransactionFactory;
        }

    }
}
