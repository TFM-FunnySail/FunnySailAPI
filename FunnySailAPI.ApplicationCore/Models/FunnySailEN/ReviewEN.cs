using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Review")]
    public class ReviewEN
    {
        [Key]
        public int Id;

        [StringLength(500)]
        [Required]
        public string Description; 

        public BoatEN Boat { get; set; }
    }
}
