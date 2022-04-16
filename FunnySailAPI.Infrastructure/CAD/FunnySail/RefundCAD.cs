using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class RefundCAD : BaseCAD<RefundEN>, IRefundCAD
    {
        public RefundCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<RefundEN> GetRefundFiltered(RefundFilters filters)
        {
            IQueryable<RefundEN> query = GetIQueryable();

            if (filters == null)
                return query;

            if (filters.Id != 0)
                query = query.Where(x => x.Id == filters.Id);

            if (filters.AmountToReturn != null)
            {
                if(filters.AmountToReturn.Max != null)
                    query = query.Where(x => x.AmountToReturn < filters.AmountToReturn.Max);

                if (filters.AmountToReturn.Min != null)
                    query = query.Where(x => x.AmountToReturn >= filters.AmountToReturn.Min);
            }

            if (filters.BookingId != 0)
                query = query.Where(x => x.BookingId >= filters.BookingId);

            if (filters.Date != null)
            {
                if(filters.Date.InitialDate != null)
                {
                    query = query.Where(x => x.Date >= filters.Date.InitialDate);
                }
                if (filters.Date.EndDate != null)
                {
                    query = query.Where(x => x.Date < filters.Date.EndDate);
                }
            }   

            if (filters.Description != null)
                query = query.Where(x => x.Description.Contains(filters.Description));

            if (filters.ClientEmail != null)
                query = query.Include(x => x.Booking.Client)
                            .ThenInclude(x => x.ApplicationUser)
                            .Where(x => x.Booking.Client.ApplicationUser.Email == filters.ClientEmail);

            return query;
        }
    }
}
