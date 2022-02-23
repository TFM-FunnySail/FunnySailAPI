using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes
{
    public class BookingOwnerInvoiceType : OwnerInvoiceCEN,IOwnerInvoiceTypes
    {
        private readonly IBookingCAD _bookingCAD;
        public BookingOwnerInvoiceType(IOwnerInvoiceCAD ownerInvoiceCAD,
                                      IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                                      IBookingCAD bookingCAD) : base(ownerInvoiceCAD,
                                                                     ownerInvoiceLineCAD)
        {
            _bookingCAD = bookingCAD;
        }

        public Task<int> CreateOwnerInvoice()
        {
            throw new NotImplementedException();
        }

        public async Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            //Buscar datos de la reserva

            //IList<OwnerInvoiceLineEN> ownerInvoiceLines = await _ownerInvoiceLineCAD.Get(filters: new OwnerInvoiceLineFilters
            //{
            //    BookingIds = addOwnerInvoiceInput.InvoiceLinesIds
            //});

            //if(ownerInvoiceLines.Count != addOwnerInvoiceInput.InvoiceLinesIds.Count) 
            //    throw new DataValidationException("The invoice lines of the reservation have not been created",
            //                                      "Las líneas de facturas de la reserva no han sido creadas");

        }
    }
}
