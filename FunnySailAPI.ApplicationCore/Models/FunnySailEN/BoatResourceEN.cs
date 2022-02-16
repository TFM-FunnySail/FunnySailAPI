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
        public int ResourceId { get; set; }

        public BoatEN Boat { get; set; }
        public ResourcesEN Resource { get; set; }
    }
}
