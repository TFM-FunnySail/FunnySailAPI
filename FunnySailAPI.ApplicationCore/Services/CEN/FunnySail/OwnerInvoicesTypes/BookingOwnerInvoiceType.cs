using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes
{
    public class BookingOwnerInvoiceType : OwnerInvoiceCEN,IOwnerInvoiceTypes
    {
        private string _ownerID;
        private decimal _amount;
        private IList<OwnerInvoiceLineEN> _ownerInvoiceLines;

        public BookingOwnerInvoiceType(IOwnerInvoiceCAD ownerInvoiceCAD,
                                      IOwnerInvoiceLineCAD ownerInvoiceLineCAD) : base(ownerInvoiceCAD,
                                                                     ownerInvoiceLineCAD)
        {
            
        }

        public async Task<int> CreateOwnerInvoice()
        {
            int newOwnerInvoice = await base.CreateOwnerInvoice(_ownerID,_amount,false);

            await _ownerInvoiceLineCAD.SetOwnerInvoice(_ownerInvoiceLines.ToList(), newOwnerInvoice);

            return newOwnerInvoice;
        }

        public async Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            //Buscar datos de la reserva
            _ownerInvoiceLines = await _ownerInvoiceLineCAD.Get(filters: new OwnerInvoiceLineFilters
            {
                BookingIds = addOwnerInvoiceInput.InvoiceLinesIds
            });

            if (_ownerInvoiceLines.Count != addOwnerInvoiceInput.InvoiceLinesIds.Count)
                throw new DataValidationException("The invoice lines of the reservation have not been created",
                                                  "Las líneas de facturas de la reserva no han sido creadas");

            if (_ownerInvoiceLines.Any(x => x.OwnerId != addOwnerInvoiceInput.OwnerId))
                throw new DataValidationException(enMessage: "There is at least one invoice line that does not belong to the owner",
                    esMessage: "Hay al menos una línea de factura que no pertenece al propietario");

            _ownerID = addOwnerInvoiceInput.OwnerId;
            _amount = _ownerInvoiceLines.Sum(x => x.Price);
        }
    }
}
