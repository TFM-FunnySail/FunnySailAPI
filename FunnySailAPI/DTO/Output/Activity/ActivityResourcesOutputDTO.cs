using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.DTO.Output.Activity
{
    public class ActivityResourcesOutputDTO
    {
        public string Uri { get; set; }
        public bool Main { get; set; }
        public ResourcesEnum Type { get; set; }
    }
}