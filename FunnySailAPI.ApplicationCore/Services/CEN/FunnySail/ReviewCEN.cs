using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ReviewCEN : IReviewCEN
    {
        private readonly IReviewCAD _reviewCAD;

        public ReviewCEN(IReviewCAD reviewCAD)
        {
            _reviewCAD = reviewCAD;
        }

        public async Task<int> AddReview(int boatId, string adminId, string observation)
        {
            ReviewEN dbReview = await _reviewCAD.AddAsync(new ReviewEN
            {
                AdminId = adminId,
                BoatId = boatId,
                Description = observation
            });

            return dbReview.Id;
        }
    }
}
