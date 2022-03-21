using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.DTO.Output.OwnerInvoice;
using System;

namespace FunnySailAPI.DTO.Output.TechnicalService
{ 
    public class TechnicalServiceBoatOutputDTO
    {
        public int Id { get; set; }
        public int BoatId { get; set; }
        public int TechnicalServiceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public bool Done { get; set; }
        public decimal Price { get; set; }
        public int? OwnerInvoiceId { get; set; }
        public BoatOutputDTO Boat { get; set; }
        public TechnicalServiceOutputDTO TechnicalService { get; set; }
        public OwnerInvoiceOutputDTO OwnerInvoice { get; set; }
    }
}