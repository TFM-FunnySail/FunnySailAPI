using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("BoatInfo")]
    public class BoatInfoEN
    {
        public int BoatId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Registration { get; set; }

        [Required]
        [StringLength(50)]
        public string MooringPoint { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Length { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Sleeve { get; set; }
        public int Capacity { get; set; }
        public int MotorPower { get; set; }

        public BoatEN Boat { get; set; }
    }
}
