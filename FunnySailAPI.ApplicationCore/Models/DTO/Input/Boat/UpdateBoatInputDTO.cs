using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class UpdateBoatInputDTO
    {
        public int BoatId { get; set; }
        public int MooringId { get; set; }
        public int BoatTypeId { get; set; }
    }
}
