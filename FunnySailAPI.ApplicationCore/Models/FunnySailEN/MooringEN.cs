using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Moorings")]
    public class MooringEN
    {
        [Key]
        public int Id { get; set; }
        public int PortId { get; set; }

        [Required, StringLength(100)]
        public string Alias { get; set; }

        public MooringEnum Type { get; set; }

        public PortEN Port { get; set; }
        public BoatEN Boat { get; set; }
    }
}
