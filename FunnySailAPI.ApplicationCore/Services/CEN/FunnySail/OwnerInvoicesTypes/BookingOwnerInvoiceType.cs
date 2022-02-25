using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
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
        private readonly IBookingCEN _bookingCEN;
        public BookingOwnerInvoiceType(IOwnerInvoiceCAD ownerInvoiceCAD,
                                      IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                                      IBookingCEN bookingCEN) : base(ownerInvoiceCAD,
                                                                     ownerInvoiceLineCAD)
        {
            _bookingCEN = bookingCEN;
        }

        public Task<int> CreateOwnerInvoice()
        {
            throw new NotImplementedException();
        }

        public async Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            //Buscar datos de la reserva
            //BookingEN bookingEN = _bookingCEN.GetBookingCAD().FindByIdAllData(addOwnerInvoiceInput.);
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
