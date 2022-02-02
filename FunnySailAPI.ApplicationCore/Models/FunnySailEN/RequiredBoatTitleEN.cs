using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("RequiredBoatTitles")]
    public class RequiredBoatTitleEN
    {
        public BoatTiteEnum TitleId { get; set; }
        public int BoatId { get; set; }

        public BoatEN Boat { get; set; }
        public BoatTitlesEnumsEN BoatTitles { get; set; }
    }
}
