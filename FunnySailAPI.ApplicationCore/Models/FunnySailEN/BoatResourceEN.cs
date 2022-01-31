using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("BoatResources")]
    public class BoatResourceEN
    {
        public int BoatId { get; set; }

        [Required]
        public string Uri { get; set; }
        public bool Main { get; set; }
        public BoatResourcesEnum Type { get; set; }

        
        public BoatEN Boat { get; set; }
    }
}
