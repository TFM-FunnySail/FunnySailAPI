using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat
{
    public class UpdateBoatTitleDTO
    {
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
