using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Mooring
{
    public class UpdateMooringDTO
    {
        public int id { get; set; }
        public string Alias { get; set; }
        public int PortId { get; set; }
        public MooringEnum Type { get; set; }
    }
}
