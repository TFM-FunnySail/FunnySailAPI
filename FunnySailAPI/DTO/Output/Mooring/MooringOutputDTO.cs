using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.DTO.Output.Port;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output.Mooring
{
    public class MooringOutputDTO
    {
        public int Id { get; set; }
        public int PortId { get; set; }
        public string Alias { get; set; }
        public bool Available { get; set; }
        public MooringEnum Type { get; set; }
        public PortOutputDTO Port { get; set; }
    }
}
