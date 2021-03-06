using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Boats")]
    public class BoatEN
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool PendingToReview { get; set; }
        public int MooringId { get; set; }
        public string OwnerId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public int BoatTypeId { get; set; }
        public BoatTypeEN BoatType { get; set; }

        public BoatInfoEN BoatInfo { get; set; }
        public BoatPricesEN BoatPrices { get; set; }
        public MooringEN Mooring { get; set; }
        public UsersEN Owner { get; set; }
        public List<BoatResourceEN> BoatResources { get; set; }
        public List<RequiredBoatTitleEN> RequiredBoatTitles { get; set; }
        public List<ReviewEN> reviews { get; set; }
        public List<BoatBookingEN> BoatBookings { get; set; }
    }
}
