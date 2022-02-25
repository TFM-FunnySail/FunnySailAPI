using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddRefundInputDTO
    {
        public int BookingId { get; set; }
        public decimal amountToReturn { get; set; }
        public string description { get; set; }
    }
}
