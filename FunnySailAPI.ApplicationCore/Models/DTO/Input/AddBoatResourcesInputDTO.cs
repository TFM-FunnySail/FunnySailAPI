using FunnySailAPI.ApplicationCore.Models.Globals;
using System.ComponentModel.DataAnnotations;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input
{
    public class AddBoatResourcesInputDTO
    {
        [Required]
        [StringLength(450)]
        public string Uri { get; set; }
        public bool Main { get; set; }
        public BoatResourcesEnum Type { get; set; }
    }
}