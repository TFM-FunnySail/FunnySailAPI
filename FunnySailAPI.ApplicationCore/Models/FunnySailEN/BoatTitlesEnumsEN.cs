using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("BoatTitlesEnums")]
    public class BoatTitlesEnumsEN
    {
        public BoatTiteEnum TitleId { get; set; }

        [Required]
        [StringLength(50)]
        public BoatTiteEnum Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public List<RequiredBoatTitleEN> RequiredBoatTitles { get; set; }
    }
}
