using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN
{
    public interface IOwnerInvoiceTypeFactory
    {
        IOwnerInvoiceTypes GetOwnerInvoiceType(OwnerInvoicesEnum type);
    }
}
