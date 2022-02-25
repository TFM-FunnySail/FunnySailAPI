using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ClientInvoiceCEN : IClientInvoiceCEN
    {
        private readonly IClientInvoiceCAD _clientInvoiceCAD;
        public async Task<int> CreateClientInvoice(ClientInvoiceEN clientInvoiceEN)
        {
            clientInvoiceEN = await _clientInvoiceCAD.AddAsync(clientInvoiceEN);

            return clientInvoiceEN.Id;
        }

        public IClientInvoiceCAD GetClientInvoiceCAD()
        {
            return _clientInvoiceCAD;
        }
    }
}
