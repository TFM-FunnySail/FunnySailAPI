using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Filters
{
    public class BoatResourceFilters
    {
        public int BoatId { get; set; }
        public int ResourceId { get; set; }
        public List<int> NotResourceId { get; set; }
        public List<int> NotBoatId { get; set; }
    }
}
