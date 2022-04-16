using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class RefundCEN : IRefundCEN
    {
        private readonly IRefundCAD _refundCAD;
        private readonly IBookingCEN _bookingCEN;

        public RefundCEN(IRefundCAD refundCAD, IBookingCEN bookingCEN)
        {
            _refundCAD = refundCAD;
            _bookingCEN = bookingCEN;
        }

        public async Task<int> CreateRefund(int bookingID,string desc, decimal amountToReturn, int? clientInvoiceId = null)
        {
            BookingEN bookingEN = await _bookingCEN.GetBookingCAD().FindById(bookingID);
            if (bookingEN == null)
                throw new DataValidationException("Booking", "Reserva", Models.Globals.ExceptionTypesEnum.NotFound);

            RefundEN dbRefund = await _refundCAD.AddAsync(new RefundEN
            {
                AmountToReturn = amountToReturn,
                BookingId = bookingID,
                Date = DateTime.UtcNow,
                Description = desc,
                ClientInvoiceId = clientInvoiceId
            });

            return dbRefund.Id;
        }
		public IRefundCAD GetRefundCAD()
        {
            return _refundCAD;
        }
		public async Task<IList<RefundEN>> GetAll(RefundFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<RefundEN>, IOrderedQueryable<RefundEN>> orderBy = null,
        Func<IQueryable<RefundEN>, IIncludableQueryable<RefundEN, object>> includeProperties = null)
        {
            var query = _refundCAD.GetRefundFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _refundCAD.Get(query, orderBy, includeProperties, pagination);
        }
        public async Task<int> GetTotal(RefundFilters filters = null)
        {
            var query = _refundCAD.GetRefundFiltered(filters);

            return await _refundCAD.GetCounter(query);
        }    }
}
