using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Port")]
    public class PortEN
    {
        [Key]
        public int Id { get; set; }
   
        public List<MooringEN> Moorings { get; set; }
    }
}
