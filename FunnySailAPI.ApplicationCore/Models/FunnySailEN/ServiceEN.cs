using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Services")]
    public class ServiceEN
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public List<ServiceBookingEN> ServiceBookings { get; set; }
    }
}
