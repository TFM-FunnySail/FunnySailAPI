using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IRefundCEN
    {
        Task<int> CreateRefund(int bookingID, string desc, decimal amountToReturn);

        public IRefundCAD GetRefundCAD();
    }
}
