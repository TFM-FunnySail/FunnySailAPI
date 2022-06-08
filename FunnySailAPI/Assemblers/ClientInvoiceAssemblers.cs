using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.ClientInvoice;
using FunnySailAPI.DTO.Output.Refund;
using FunnySailAPI.DTO.Output.User;
using System;
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
                TotalAmount = Math.Round(clientInvoiceEN.TotalAmount, 2),
                CreatedDate = clientInvoiceEN.CreatedDate,
                Canceled = clientInvoiceEN.Canceled,
                Paid = clientInvoiceEN.Paid,
                ClientId = clientInvoiceEN.ClientId
            };

            if (clientInvoiceEN.Client != null)
            {
                clientInvoiceEN.Client.Invoices = null;
                clientInvoiceOutput.Client = UserAssemblers.Convert(clientInvoiceEN.Client);
            }
                

            if (clientInvoiceEN.InvoiceLines != null)
            {
                clientInvoiceOutput.InvoiceLines = clientInvoiceEN.InvoiceLines.Select(x => new ClientInvoiceLinesOutputDTO
                {
                    BookingId = x.BookingId,
                    TotalAmount = Math.Round(x.TotalAmount, 2),
                    ClientInvoiceId = x.ClientInvoiceId,
                    Currency = x.Currency.ToString()
                }).ToList();
            }


            if (clientInvoiceEN.Refunds != null)
            {
                clientInvoiceOutput.Refunds = clientInvoiceEN.Refunds.Select(x => new RefundOutputDTO
                {
                    Id = x.Id,
                    AmountToReturn = Math.Round(x.AmountToReturn, 2),
                    BookingId = x.BookingId,
                    Date = x.Date,
                    Description = x.Description
                }).ToList();
            }

            return clientInvoiceOutput;
        }
    }
}
