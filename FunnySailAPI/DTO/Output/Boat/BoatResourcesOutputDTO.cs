using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.DTO.Output.Boat
{
    public class BoatResourcesOutputDTO
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public bool Main { get; set; }
        public ResourcesEnum Type { get; set; }
    }
}