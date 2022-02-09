using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("TechnicalServiceBoat")]
    public class TechnicalServiceBoatEN
    {
        public int TechnicalServiceId { get; set; }
        public int BoatId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public TechnicalServiceEN TechnicalService { get; set; }
        public BoatEN Boat { get; set; }
    }
}
