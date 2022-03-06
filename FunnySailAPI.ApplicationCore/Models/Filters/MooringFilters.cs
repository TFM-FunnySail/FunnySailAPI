using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class MooringFilters
    {
        public int? MooringId { get; set; }
        public int? PortId { get; set; }
        public List<int> MooringIdList { get; set; }
        public string Alias { get; set; }
        public MooringEnum? Type { get; set; }
    }
}
