using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class UpdateBoatInputDTO
    {
        public int BoatId { get; set; }
        public int? MooringId { get; set; }
        public int? BoatTypeId { get; set; }
        public bool? Active { get; set; }
        public bool? PendingToReview { get; set; }
        public UpdateBoatInfoInputDTO BoatInfo { get; set; }
        public UpdateRequiredBoatTitleDTO RequiredTitles { get; set; }
        public UpdateBoatPricesInputDTO Prices { get; set; }
    }
}
