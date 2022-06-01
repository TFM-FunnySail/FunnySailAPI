using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.OwnerInvoice;
using System;

namespace FunnySailAPI.Assemblers
{
    public static class OwnerInvoiceLineAssemblers
    {
        public static OwnerInvoiceLinesOutputDTO Convert(OwnerInvoiceLineEN ownerInvoiceEN)
        {
            OwnerInvoiceLinesOutputDTO ownerInvoiceOutput = new OwnerInvoiceLinesOutputDTO
            {
                BookingId = ownerInvoiceEN.BookingId,
                OwnerId = ownerInvoiceEN.OwnerId,
                OwnerInvoiceId = ownerInvoiceEN.OwnerInvoiceId,
                Price = Math.Round(ownerInvoiceEN.Price, 2),
            };

            if (ownerInvoiceEN.Owner != null)
                ownerInvoiceOutput.Owner = UserAssemblers.Convert(ownerInvoiceEN.Owner);

            if(ownerInvoiceEN.Booking != null)
                ownerInvoiceOutput.Booking = BookingAssemblers.Convert(ownerInvoiceEN.Booking);

            return ownerInvoiceOutput;
        }
    }
}
