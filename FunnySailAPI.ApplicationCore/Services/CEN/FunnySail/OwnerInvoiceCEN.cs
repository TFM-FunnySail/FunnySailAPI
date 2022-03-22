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
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class OwnerInvoiceCEN : IOwnerInvoiceCEN
    {
        protected readonly IOwnerInvoiceCAD _ownerInvoiceCAD;
        protected readonly IOwnerInvoiceLineCAD _ownerInvoiceLineCAD;
        protected readonly IUserCAD _userCAD;
        private readonly string _enName;
        private readonly string _esName;
        public OwnerInvoiceCEN(IOwnerInvoiceCAD ownerInvoiceCAD,
                               IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                               IUserCAD userCAD)
        {
            _ownerInvoiceCAD = ownerInvoiceCAD;
            _enName = "Owner invoice";
            _esName = "Factura de propietario";
            _ownerInvoiceLineCAD = ownerInvoiceLineCAD;
            _userCAD = userCAD;
        }

        public async Task<OwnerInvoiceEN> CancelOwnerInvoice(int ownerInvoiceId)
        {
            OwnerInvoiceEN ownerInvoiceEN = await _ownerInvoiceCAD.FindById(ownerInvoiceId);
            
            if(ownerInvoiceEN == null)
                throw new DataValidationException(_enName, _esName, ExceptionTypesEnum.NotFound);
            if (ownerInvoiceEN.IsPaid)
                throw new DataValidationException(
                    $"The {_enName} cannot be canceled because it has already been paid",
                    $"No se puede cancelar la {_esName} porque ya fue pagada.");

            if (ownerInvoiceEN.IsCanceled) return ownerInvoiceEN;

            ownerInvoiceEN.IsCanceled = true;

            await _ownerInvoiceCAD.Update(ownerInvoiceEN);

            return ownerInvoiceEN;
        }

        public async Task<int> CreateOwnerInvoice(string ownerId,decimal amount,bool toCollet)
        {
            UsersEN user = await _userCAD.FindById(ownerId);

            if (user == null)
                throw new DataValidationException("Owner", "Propietario", ExceptionTypesEnum.NotFound);

            OwnerInvoiceEN ownerInvoiceEN = await _ownerInvoiceCAD.AddAsync(new OwnerInvoiceEN
            {
                Date = DateTime.UtcNow,
                Amount = amount,
                IsCanceled = false,
                IsPaid = false,
                ToCollet = toCollet,
                OwnerId = ownerId
            });

            return ownerInvoiceEN.Id;
        }

        public IOwnerInvoiceLineCAD GetOwnerInvoiceLineCAD()
        {
            return _ownerInvoiceLineCAD;
        }

       public async Task<IList<OwnerInvoiceEN>> GetAll(OwnerInvoiceFilters filters = null,
       Pagination pagination = null,
       Func<IQueryable<OwnerInvoiceEN>, IOrderedQueryable<OwnerInvoiceEN>> orderBy = null,
       Func<IQueryable<OwnerInvoiceEN>, IIncludableQueryable<OwnerInvoiceEN, object>> includeProperties = null)
        {
            var OwnerInvoices = _ownerInvoiceCAD.GetOwnerInvoiceFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await _ownerInvoiceCAD.Get(OwnerInvoices, orderBy, includeProperties, pagination);
        }

        public async Task<int> GetTotal(OwnerInvoiceFilters filters = null)
        {
            var OwnerInvoices = _ownerInvoiceCAD.GetOwnerInvoiceFiltered(filters);

            return await _ownerInvoiceCAD.GetCounter(OwnerInvoices);
        }
    }
}
