using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.Refund;
using FunnySailAPI.DTO.Output.User;
using System.Linq;

namespace FunnySailAPI.Assemblers
{
    public static class ClientInvoiceAssemblers
    {
        public static ClientInvoiceOutputDTO Convert(ClientInvoiceEN clientInvoiceEN)
        {
            ClientInvoiceOutputDTO clientInvoiceOutput = new ClientInvoiceOutputDTO
            {
                Id = clientInvoiceEN.Id,
                TotalAmount = clientInvoiceEN.TotalAmount,
                CreatedDate = clientInvoiceEN.CreatedDate,
                Canceled = clientInvoiceEN.Canceled,
                Paid = clientInvoiceEN.Paid,
                ClientId = clientInvoiceEN.ClientId
            };

            if (clientInvoiceEN.Client != null)
                clientInvoiceOutput.Client = UserAssemblers.Convert(clientInvoiceEN.Client);

            if (clientInvoiceEN.InvoiceLines != null)
            {
                clientInvoiceOutput.InvoiceLines = clientInvoiceEN.InvoiceLines.Select(x => new ClientInvoiceLinesOutputDTO
                {
                    BookingId = x.BookingId,
                    TotalAmount = x.TotalAmount,
                    ClientInvoiceId = x.ClientInvoiceId,
                    Currency = x.Currency.ToString()
                }).ToList();
            }


            if (clientInvoiceEN.Refunds != null)
            {
                clientInvoiceOutput.Refunds = clientInvoiceEN.Refunds.Select(x => new RefundOutputDTO
                {
                    Id = x.Id,
                    AmountToReturn = x.AmountToReturn,
                    BookingId = x.BookingId,
                    Date = x.Date,
                    Description = x.Description
                }).ToList();
            }

            return clientInvoiceOutput;
        }
    }
}
