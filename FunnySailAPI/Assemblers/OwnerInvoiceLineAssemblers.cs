using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.OwnerInvoice;

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
                Price = ownerInvoiceEN.Price,
            };

            if (ownerInvoiceEN.Owner != null)
                ownerInvoiceOutput.Owner = UserAssemblers.Convert(ownerInvoiceEN.Owner);


            return ownerInvoiceOutput;
        }
    }
}
