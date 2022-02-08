using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Reviews")]
    public class ReviewEN
    {
        [Key]
        public int Id { get; set; }
        public int BoatId { get; set; }
        public string AdminId { get; set; } //Admin que hizo la revision

        [Required, StringLength(500)]
        public string Description { get; set; }

        public BoatEN Boat { get; set; }
        public UsersEN Admin { get; set; }
    }
}
