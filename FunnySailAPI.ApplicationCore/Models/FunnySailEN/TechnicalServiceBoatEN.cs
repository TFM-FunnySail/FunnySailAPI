using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("TechnicalServiceBoat")]
    public class TechnicalServiceBoatEN
    {
        public int BoatId { get; set; }

        public int TechnicalServiceId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        public bool Done { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public BoatEN Boat { get; set; }

        public TechnicalServiceEN TechnicalService { get; set; }
    }
}
