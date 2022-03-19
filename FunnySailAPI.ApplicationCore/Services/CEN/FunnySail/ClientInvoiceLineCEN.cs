using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ClientInvoiceLineCEN : IClientInvoiceLineCEN
    {
        private readonly IClientInvoiceLineCAD _invoiceLineCAD;

        public ClientInvoiceLineCEN(IClientInvoiceLineCAD invoiceLineCAD)
        {
            _invoiceLineCAD = invoiceLineCAD;
        }

        public async Task<int> CreateInvoiceLine(InvoiceLineEN invoiceLineEN)
        {
            invoiceLineEN = await _invoiceLineCAD.AddAsync(invoiceLineEN);

            return invoiceLineEN.BookingId;
        }

        public IClientInvoiceLineCAD GetInvoiceLineCAD()
        {
            return _invoiceLineCAD;
        }
    }
}
