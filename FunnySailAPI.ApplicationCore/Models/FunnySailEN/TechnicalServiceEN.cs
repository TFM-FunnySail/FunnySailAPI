using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("TechnicalService")]
    public class TechnicalServiceEN
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [Required]
        public bool Active { get; set; }

        public List<TechnicalServiceBoatEN> TechnicalServicesBoat { get; set; }
    }
}
