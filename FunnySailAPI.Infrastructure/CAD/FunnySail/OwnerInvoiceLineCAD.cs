using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class OwnerInvoiceLineCAD : BaseCAD<OwnerInvoiceLineEN>,IOwnerInvoiceLineCAD
    {
        public OwnerInvoiceLineCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
