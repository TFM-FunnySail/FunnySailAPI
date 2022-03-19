using FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IOwnerInvoiceCP
    {
        Task<int> CreateOwnerInvoice(AddOwnerInvoiceInputDTO addOwnerInvoiceInput);
    }
}
