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
        public int Id;
        public string AdminId; //Admin que hizo la revision

        [StringLength(500)]
        [Required]
        public string Description; 

        public BoatEN Boat { get; set; }
        public UsersEN Admin { get; set; }
    }
}
