using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Refund;

namespace FunnySailAPI.Assemblers
{
    public static class RefundAssemblers
    {
        public static RefundOutputDTO Convert(RefundEN refund)
        {
            RefundOutputDTO refundOutput = new RefundOutputDTO
            {
                AmountToReturn = refund.AmountToReturn,
                BookingId = refund.BookingId,
                ClientInvoiceId = refund.ClientInvoiceId,
                Date = refund.Date,
                Description = refund.Description,
                Id = refund.Id,
            };

            if (refund.ClientInvoice != null)
            {
                refund.ClientInvoice.Refunds = null;
                refundOutput.ClientInvoice = ClientInvoiceAssemblers.Convert(refund.ClientInvoice);
            }

            if(refund.Booking?.Client != null)
            {
                refund.Booking.Client.Bookings = null;
                refund.Booking.Client.Invoices = null;
                refundOutput.Client = UserAssemblers.Convert(refund.Booking.Client);
            }

            return refundOutput;
        }
    }
}
