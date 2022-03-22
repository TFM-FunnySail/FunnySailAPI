using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ClientInvoiceCEN : IClientInvoiceCEN
    {
  
        protected readonly IClientInvoiceCAD _clientInvoiceCAD;
        protected readonly IClientInvoiceLineCAD _clientInvoiceLineCAD;
        private readonly string _enName;
        private readonly string _esName;
        public ClientInvoiceCEN(IClientInvoiceCAD clientInvoiceCAD,
                               IClientInvoiceLineCAD clientInvoiceLineCAD)
        {
            _clientInvoiceCAD = clientInvoiceCAD;
            _enName = "Client invoice";
            _esName = "Factura de cliente";
            _clientInvoiceLineCAD = clientInvoiceLineCAD;
        }
        
        public async Task<ClientInvoiceEN> CancelClientInvoice(int clientInvoiceId)
        {
     
            ClientInvoiceEN clientInvoiceEN = await _clientInvoiceCAD.FindById(clientInvoiceId);

            if (clientInvoiceEN == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);
            if (clientInvoiceEN.Paid)
                throw new DataValidationException(
                    $"The {_enName} cannot be canceled because it has already been paid",
                    $"No se puede cancelar la {_esName} porque ya fue pagada.");

            if (clientInvoiceEN.Canceled) return clientInvoiceEN;

            clientInvoiceEN.Canceled = true;

            await _clientInvoiceCAD.Update(clientInvoiceEN);

            return clientInvoiceEN;
            
        }

        public async Task<int> CreateClientInvoice(ClientInvoiceEN clientInvoiceEN)
        {
            clientInvoiceEN = await _clientInvoiceCAD.AddAsync(clientInvoiceEN);

            return clientInvoiceEN.Id;
        }
 
        public IClientInvoiceCAD GetClientInvoiceCAD()
        {
            return _clientInvoiceCAD;
        }

        public async Task<IList<ClientInvoiceEN>> GetAll(ClientInvoiceFilters filters = null,
        Pagination pagination = null,
        Func<IQueryable<ClientInvoiceEN>, IOrderedQueryable<ClientInvoiceEN>> orderBy = null,
        Func<IQueryable<ClientInvoiceEN>, IIncludableQueryable<ClientInvoiceEN, object>> includeProperties = null)
        {
            var ClientInvoices = _clientInvoiceCAD.GetClientInvoiceFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _clientInvoiceCAD.Get(ClientInvoices, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(ClientInvoiceFilters filters = null)
        {
            var ClientInvoices = _clientInvoiceCAD.GetClientInvoiceFiltered(filters);

            return await _clientInvoiceCAD.GetCounter(ClientInvoices);
        }
    }
}
