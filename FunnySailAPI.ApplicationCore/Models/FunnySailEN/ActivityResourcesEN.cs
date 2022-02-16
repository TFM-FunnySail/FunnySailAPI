using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    public class ActivityResourcesEN
    {
        public int ActivityId { get; set; }
        public int ResourceId { get; set; }

        public ActivityEN Activity { get; set; }
        public ResourcesEN Resource { get; set; }
    }
}
