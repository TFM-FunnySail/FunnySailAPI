using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat
{
    public class UpdateRequiredBoatTitleDTO
    {
        public int BoatId { get; set; }
        public List<BoatTiteEnum> BoatTites { get; set; }
    }
}
