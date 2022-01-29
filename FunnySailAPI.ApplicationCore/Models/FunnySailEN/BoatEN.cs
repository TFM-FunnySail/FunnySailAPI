using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Boat")]
    public class BoatEN
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool PendingToReview { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public int BoatTypeId { get; set; }
        public BoatTypeEN BoatType { get; set; }

        public List<BoatResourceEN> BoatResources { get; set; }
    }
}
