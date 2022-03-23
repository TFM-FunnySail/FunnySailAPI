using FunnySailAPI.DTO.Output.Boat;
using FunnySailAPI.DTO.Output.User;

namespace FunnySailAPI.DTO.Output.Review
{
    public class ReviewOutputDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Closed { get; set; }

        public BoatOutputDTO Boat { get; set; }
        public UserOutputDTO Admin { get; set; }
    }
}
