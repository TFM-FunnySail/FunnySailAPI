using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IInvoiceLineCAD : IBaseCAD<InvoiceLineEN>
    {
        Task<InvoiceLineEN> FindByIds(int idBooking, int? idClientInvoice);
    }
}
