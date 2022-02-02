using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Output
{
    public class BoatResourcesOutputDTO
    {
        public string Uri { get; set; }
        public bool Main { get; set; }
        public BoatResourcesEnum Type { get; set; }
    }
}