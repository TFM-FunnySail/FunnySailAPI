using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Review;

namespace FunnySailAPI.Assemblers
{
    public static class ReviewAssemblers
    {
        public static ReviewOutputDTO Convert(ReviewEN review)
        {
            ReviewOutputDTO reviewOutputDTO = new ReviewOutputDTO
            {
                Id = review.Id,
            };

            if(review.Admin != null)
                reviewOutputDTO.Admin = UserAssemblers.Convert(review.Admin);

            if (review.Boat != null)
                reviewOutputDTO.Boat = BoatAssemblers.Convert(review.Boat);

            return reviewOutputDTO;
        }
    }
}
