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
        public int id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        [Required]
        public string Description { get; set; }
    }
   
}
