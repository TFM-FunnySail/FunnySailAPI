using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes
{
    public class TecServiceOwnerInvoiceType : OwnerInvoiceCEN, IOwnerInvoiceTypes
    {
        private string _ownerID;
        private decimal _amount;
        private List<TechnicalServiceBoatEN> _technicalServiceBoats;
        private readonly ITechnicalServiceBoatCAD _technicalServiceBoatCAD;

        public TecServiceOwnerInvoiceType(IOwnerInvoiceCAD ownerInvoiceCAD,
                                         IOwnerInvoiceLineCAD ownerInvoiceLineCAD,
                                         ITechnicalServiceBoatCAD technicalServiceBoatCAD) : base(ownerInvoiceCAD,
                                                                                          ownerInvoiceLineCAD)
        {
            _technicalServiceBoatCAD = technicalServiceBoatCAD;
        }

        public async Task<int> CreateOwnerInvoice()
        {
            int newOwnerInvoice = await base.CreateOwnerInvoice(_ownerID, _amount, true);

            await _technicalServiceBoatCAD.SetOwnerInvoice(_technicalServiceBoats, newOwnerInvoice);

            return newOwnerInvoice;
        }

        public async Task ValidateAndPrepare(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            //Buscar datos de la reserva
            _technicalServiceBoats = (await _technicalServiceBoatCAD.Get(filters: new TechnicalServiceBoatFilters
            {
                IdList = addOwnerInvoiceInput.InvoiceLinesIds
            },includeProperties:"Boat,")).ToList();

            if (_technicalServiceBoats.Count != addOwnerInvoiceInput.InvoiceLinesIds.Count)
                throw new DataValidationException("The invoice lines of the reservation have not been created",
                                                  "Las líneas de facturas de la reserva no han sido creadas");

            if (_technicalServiceBoats.Any(x => x.Boat.OwnerId != addOwnerInvoiceInput.OwnerId))
                throw new DataValidationException(enMessage: "There is at least one invoice line that does not belong to the owner",
                    esMessage: "Hay al menos una línea de factura que no pertenece al propietario");

            _ownerID = addOwnerInvoiceInput.OwnerId;
            _amount = _technicalServiceBoats.Sum(x => x.Price);
        }
    }
}
