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
        public int Name { get; set; }

        [Required, StringLength(1000)]
        public int Location { get; set; }

        public List<MooringEN> Moorings { get; set; }
    }
}
