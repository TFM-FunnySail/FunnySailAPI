using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class OwnerInvoiceCEN : IOwnerInvoiceCEN
    {
        private readonly IOwnerInvoiceCAD _ownerInvoiceCAD;
        private readonly string _enName;
        private readonly string _esName;
        public OwnerInvoiceCEN(IOwnerInvoiceCAD ownerInvoiceCAD)
        {
            _ownerInvoiceCAD = ownerInvoiceCAD;
            _enName = "Owner invoice";
            _esName = "Factura de propietario";
        }

        public async Task<OwnerInvoiceEN> CancelOwnerInvoice(int ownerInvoiceId)
        {
            OwnerInvoiceEN ownerInvoiceEN = await _ownerInvoiceCAD.FindById(ownerInvoiceId);
            if(ownerInvoiceEN == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);

            if (!ownerInvoiceEN.IsCanceled) return ownerInvoiceEN;

            ownerInvoiceEN.IsCanceled = true;

            await _ownerInvoiceCAD.Update(ownerInvoiceEN);

            return ownerInvoiceEN;
        }
    }
}
