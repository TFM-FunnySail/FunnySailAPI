using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes
{
    public class OwnerInvoiceTypeFactory : IOwnerInvoiceTypeFactory
    {
        private readonly IOwnerInvoiceCAD _ownerInvoiceCAD;
        private readonly IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        private readonly IBookingCAD _bookingCAD;

        public OwnerInvoiceTypeFactory(IOwnerInvoiceCAD ownerInvoiceCAD,
                                       IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                                       IBookingCAD bookingCAD)
        {
            _ownerInvoiceCAD = ownerInvoiceCAD;
            _ownerInvoiceLineCAD = ownerInvoiceLineCAD;
            _bookingCAD = bookingCAD;
        }

        public IOwnerInvoiceTypes GetOwnerInvoiceType(OwnerInvoicesEnum type)
        {
            switch (type)
            {
                case OwnerInvoicesEnum.Booking:
                    return new BookingOwnerInvoiceType(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _bookingCAD);

                case OwnerInvoicesEnum.TechnicalService:
                    return new TecServiceOwnerInvoiceType(_ownerInvoiceCAD, _ownerInvoiceLineCAD);
            }

            throw new NotImplementedException();
        }
    }
}
