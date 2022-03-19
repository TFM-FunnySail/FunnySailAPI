using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{

    [Table("Activity")]
    public class ActivityEN
    {
        [Key]
        public int Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public String Name { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        public List<ActivityResourcesEN> ActivityResources { get; set; }

        public List<ActivityBookingEN> ActivityBookings { get; set; }
    }
}
