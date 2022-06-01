using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Activity;
using FunnySailAPI.DTO.Output.OwnerInvoice;
using FunnySailAPI.DTO.Output.TechnicalService;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class OwnerInvoiceAssemblers
    {
        public static OwnerInvoiceOutputDTO Convert(OwnerInvoiceEN ownerInvoiceEN)
        {
            OwnerInvoiceOutputDTO ownerInvoiceOutput = new OwnerInvoiceOutputDTO
            {
                Id = ownerInvoiceEN.Id,
                Amount = Math.Round(ownerInvoiceEN.Amount,2),
                Date = ownerInvoiceEN.Date,
                IsCanceled = ownerInvoiceEN.IsCanceled,
                IsPaid = ownerInvoiceEN.IsPaid,
                ToCollet = ownerInvoiceEN.ToCollet,
                OwnerId = ownerInvoiceEN.OwnerId,
            };

            if (ownerInvoiceEN.Owner != null)
                ownerInvoiceOutput.Owner = UserAssemblers.Convert(ownerInvoiceEN.Owner);

            if (ownerInvoiceEN.OwnerInvoiceLines != null)
            {
                ownerInvoiceOutput.OwnerInvoiceLines = new List<OwnerInvoiceLinesOutputDTO>();
                foreach (var line in ownerInvoiceEN.OwnerInvoiceLines)
                {
                    line.Owner = null;
                    ownerInvoiceOutput.OwnerInvoiceLines.Add(OwnerInvoiceLineAssemblers.Convert(line));
                }
            }

            if (ownerInvoiceEN.TechnicalServiceBoats != null)
            {
                ownerInvoiceOutput.TechnicalServiceBoats = ownerInvoiceEN.TechnicalServiceBoats.Select(x => new TechnicalServiceBoatOutputDTO
                {
                    BoatId = x.BoatId,
                    OwnerInvoiceId = x.OwnerInvoiceId,
                    Price = Math.Round(x.Price, 2),
                    Id = x.Id,
                    Done = x.Done,
                    CreatedDate = x.CreatedDate,
                    TechnicalServiceId = x.TechnicalServiceId,
                    ServiceDate = x.ServiceDate,
                }).ToList();
            }

            return ownerInvoiceOutput;
        }
    }
}
