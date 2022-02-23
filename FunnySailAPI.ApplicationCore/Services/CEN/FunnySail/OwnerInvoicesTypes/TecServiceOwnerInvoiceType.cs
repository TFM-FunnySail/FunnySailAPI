using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes
{
    public class TecServiceOwnerInvoiceType : OwnerInvoiceCEN, IOwnerInvoiceTypes
    {
        public TecServiceOwnerInvoiceType(IOwnerInvoiceCAD ownerInvoiceCAD,
                                         IOwnerInvoiceLineCAD ownerInvoiceLineCAD) : base(ownerInvoiceCAD,
                                                                                          ownerInvoiceLineCAD)
        {

        }

        public Task<int> CreateOwnerInvoice()
        {
            throw new NotImplementedException();
        }

        public Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            throw new NotImplementedException();
        }
    }
}
