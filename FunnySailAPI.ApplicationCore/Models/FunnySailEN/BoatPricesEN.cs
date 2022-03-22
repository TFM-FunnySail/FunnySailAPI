using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("BoatPrices")]
    public class BoatPricesEN
    {
        public int BoatId { get; set; }

        [Column(TypeName = "money")]
        public decimal DayBasePrice { get; set; }

        [Column(TypeName = "money")]
        public decimal HourBasePrice { get; set; }

        public float Supplement { get; set; }
        public float PorcentPriceOwner { get; set; }

        public BoatEN Boat { get; set; }
    }
}
