using FunnySailAPI.DTO.Output.Mooring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output.Port
{
    public class PortOutputDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public List<MooringOutputDTO> Moorings { get; set; }
    }
}
