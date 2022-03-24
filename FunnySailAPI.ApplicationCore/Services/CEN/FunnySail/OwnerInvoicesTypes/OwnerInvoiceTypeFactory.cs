using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
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
        private readonly ITechnicalServiceBoatCAD _technicalServiceBoatCAD;
        private readonly IUserCAD _userCAD;
        public OwnerInvoiceTypeFactory(IOwnerInvoiceCAD ownerInvoiceCAD,
                                       IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                                       IUserCAD userCAD,
                                       ITechnicalServiceBoatCAD technicalServiceBoatCAD)
        {
            _ownerInvoiceCAD = ownerInvoiceCAD;
            _ownerInvoiceLineCAD = ownerInvoiceLineCAD;
            _technicalServiceBoatCAD = technicalServiceBoatCAD;
            _userCAD = userCAD;
        }

        public IOwnerInvoiceTypes GetOwnerInvoiceType(OwnerInvoicesEnum type)
        {
            switch (type)
            {
                case OwnerInvoicesEnum.Booking:
                    return new BookingOwnerInvoiceType(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _userCAD);

                case OwnerInvoicesEnum.TechnicalService:
                    return new TecServiceOwnerInvoiceType(_ownerInvoiceCAD, _ownerInvoiceLineCAD, _userCAD, _technicalServiceBoatCAD);
            }

            throw new NotImplementedException();
        }
    }
}
