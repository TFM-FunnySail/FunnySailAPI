using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat
{
    public class UpdateBoatPricesInputDTO
    {
        public int BoatId { get; set; }
        public decimal DayBasePrice { get; set; }
        public decimal HourBasePrice { get; set; }
        public float Supplement { get; set; }
    }
}
