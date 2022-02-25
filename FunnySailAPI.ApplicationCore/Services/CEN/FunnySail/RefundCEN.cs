using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class RefundCEN : IRefundCEN
    {
        private readonly IRefundCAD _refundCAD;

        public RefundCEN(IRefundCAD refundCAD)
        {
            _refundCAD = refundCAD;
        }

        public async Task<int> CreateRefund(int bookingID,string desc, decimal amountToReturn)
        {
            RefundEN dbRefund = await _refundCAD.AddAsync(new RefundEN
            {
                AmountToReturn = amountToReturn,
                BookingId = bookingID,
                Date = DateTime.UtcNow,
                Description = desc
            });

            return dbRefund.Id;
        }
    }
}
