using FunnySailAPI.ApplicationCore.Models.Globals;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddBoatResourcesInputDTO
    {
        public string Uri { get; set; }
        public bool Main { get; set; }
        public BoatResourcesEnum Type { get; set; }
    }
}