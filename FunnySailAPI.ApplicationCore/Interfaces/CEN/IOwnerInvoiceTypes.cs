using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.OwnerInvoice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN
{
    public interface IOwnerInvoiceTypes
    {
        Task<int> CreateOwnerInvoice();
        Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput);
    }
}
