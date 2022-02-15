using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Ports")]
    public class PortEN
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(100)]
        public String Name { get; set; }

        [Required, StringLength(1000)]
        public String Location { get; set; }

        public List<MooringEN> Moorings { get; set; }
    }
}
