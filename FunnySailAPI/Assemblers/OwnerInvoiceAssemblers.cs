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
                Amount = ownerInvoiceEN.Amount,
                Date = ownerInvoiceEN.Date,
                IsCanceled = ownerInvoiceEN.IsCanceled,
                IsPaid = ownerInvoiceEN.IsPaid,
                ToCollet = ownerInvoiceEN.ToCollet,
                OwnerId = ownerInvoiceEN.OwnerId,
            };

            if(ownerInvoiceEN.Owner != null)
            {
                ownerInvoiceOutput.Owner = new UserOutputDTO
                {
                    UserId = ownerInvoiceEN.Owner.UserId,
                    BirthDay = ownerInvoiceEN.Owner.BirthDay,
                    BoatOwner = ownerInvoiceEN.Owner.BoatOwner,
                    FirstName = ownerInvoiceEN.Owner.FirstName,
                    LastName = ownerInvoiceEN.Owner.LastName,
                    ReceivePromotion = ownerInvoiceEN.Owner.ReceivePromotion,
                    EmailConfirmed = ownerInvoiceEN.Owner.ApplicationUser.EmailConfirmed,
                    Email = ownerInvoiceEN.Owner.ApplicationUser.Email,
                    PhoneNumber = ownerInvoiceEN.Owner.ApplicationUser.PhoneNumber,
                    UserName = ownerInvoiceEN.Owner.ApplicationUser.UserName,
                };
            }

            if (ownerInvoiceEN.OwnerInvoiceLines != null)
            {
                ownerInvoiceOutput.OwnerInvoiceLines = ownerInvoiceEN.OwnerInvoiceLines.Select(x => new OwnerInvoiceLinesOutputDTO
                {
                   BookingId = x.BookingId,
                   OwnerInvoiceId = x.OwnerInvoiceId,
                   Price = x.Price,
                   OwnerId = x.OwnerId,
                }).ToList();
            }

            if (ownerInvoiceEN.TechnicalServiceBoats != null)
            {
                ownerInvoiceOutput.TechnicalServiceBoats = ownerInvoiceEN.TechnicalServiceBoats.Select(x => new TechnicalServiceBoatOutputDTO
                {
                    BoatId = x.BoatId,
                    OwnerInvoiceId = x.OwnerInvoiceId,
                    Price = x.Price,
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
