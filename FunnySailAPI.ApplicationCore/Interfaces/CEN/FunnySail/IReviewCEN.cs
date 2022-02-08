using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IReviewCEN
    {
        Task<int> AddReview(int boatId, string adminId, string observation);
    }
}
