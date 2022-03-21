using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.ClientInvoice;
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
                ClientId = clientInvoiceEN.ClientId,
            };

            if(clientInvoiceEN.Client != null)
            {
                clientInvoiceOutput.Client = new UserOutputDTO
                {
                    UserId = clientInvoiceEN.Client.UserId,
                    BirthDay = clientInvoiceEN.Client.BirthDay,
                    BoatOwner = clientInvoiceEN.Client.BoatOwner,
                    FirstName = clientInvoiceEN.Client.FirstName,
                    LastName = clientInvoiceEN.Client.LastName,
                    ReceivePromotion = clientInvoiceEN.Client.ReceivePromotion,
                    EmailConfirmed = clientInvoiceEN.Client.ApplicationUser.EmailConfirmed,
                    Email = clientInvoiceEN.Client.ApplicationUser.Email,
                    PhoneNumber = clientInvoiceEN.Client.ApplicationUser.PhoneNumber,
                    UserName = clientInvoiceEN.Client.ApplicationUser.UserName,
                };
            }

            if (clientInvoiceEN.InvoiceLines != null)
            {
                clientInvoiceOutput.InvoiceLines = clientInvoiceEN.InvoiceLines.Select(x => new ClientInvoiceLinesOutputDTO
                {
                   BookingId = x.BookingId,
                   TotalAmount = x.TotalAmount,
                   ClientInvoiceId = x.ClientInvoiceId,
                }).ToList();
            }

            return clientInvoiceOutput;
        }
    }
}
