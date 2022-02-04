using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("Mooring")]
    public class MooringEN
    {
        [Key]
        public int Id { get; set; }
        
        public MooringEnum Type { get; set; }
    }
}
