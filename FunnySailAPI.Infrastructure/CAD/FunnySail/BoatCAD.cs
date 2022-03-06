using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class BoatCAD : BaseCAD<BoatEN>, IBoatCAD
    {
        public BoatCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        #region AnyQuery

        public async Task<bool> AnyById(int boatID)
        {
            return await _dbContext.Boats.AnyAsync(x => x.Id == boatID);
        }

        public async Task<bool> IsBoatBusy(int boatId, DateTime serviceDate)
        {
            bool technicalServiceBusy = await _dbContext.TechnicalServiceBoat.
                AnyAsync(x => x.BoatId == boatId && x.ServiceDate == serviceDate);

            if (technicalServiceBusy)
                return technicalServiceBusy;

            return await _dbContext.BoatBookings.Where(x => x.BoatId == boatId)
                .Join(_dbContext.Bookings.
                Where(x => x.EntryDate < serviceDate && x.DepartureDate > serviceDate),
                bb => bb.BookingId,
                b => b.Id,
                (bb, b) => bb.BoatId).AnyAsync();
        }
        #endregion

        #region GetQuery

        public async Task<BoatEN> FindByIdAllData(int boatId)
        {
            return await _dbContext.Boats
                .Include(x => x.BoatInfo)
                .Include(x => x.BoatPrices)
                .Include(x => x.BoatType)
                .Include(x => x.BoatResources)
                .Include(x => x.RequiredBoatTitles)
                .FirstOrDefaultAsync(x => x.Id == boatId);
        }

        public async Task<List<int>> GetBoatIdsNotAvailable(DateTime initialDate, DateTime endDate)
        {
            return await _dbContext.Bookings.
                Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
                (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
                .Join(_dbContext.BoatBookings,
                b => b.Id, bb => bb.BookingId,
                (booking, boatBooking) => boatBooking.BoatId)
                .Distinct().ToListAsync();
        }

        public async Task<List<int>> GetBoatIdsNotAvailable(DateTime initialDate, DateTime endDate,List<int> ids)
        {
            return await _dbContext.Bookings.
                Where(x => (x.EntryDate >= initialDate && x.EntryDate <= endDate) ||
                (x.DepartureDate > initialDate && x.DepartureDate <= endDate))
                .Join(_dbContext.BoatBookings.Where(x=> ids.Contains(x.BoatId)),
                b => b.Id, bb => bb.BookingId,
                (booking, boatBooking) => boatBooking.BoatId)
                .Distinct().ToListAsync();
        }

        #endregion

        #region Filter
        public IQueryable<BoatEN> GetBoatFiltered(BoatFilters boatFilters)
        {
            IQueryable<BoatEN> boats = GetIQueryable();

            if (boatFilters == null)
                return boats;

            if (boatFilters.BoatId != 0)
                boats = boats.Where(x => x.Id == boatFilters.BoatId);

            if (boatFilters.BoatTypeId != 0)
                boats = boats.Where(x => x.BoatTypeId == boatFilters.BoatTypeId);

            if (boatFilters.Active != null)
                boats = boats.Where(x => x.Active == boatFilters.Active);

            if (boatFilters.PendingToReview != null)
                boats = boats.Where(x => x.PendingToReview == boatFilters.PendingToReview);

            if (boatFilters.CreatedDaysRange?.InitialDate != null)
                boats = boats.Where(x => x.CreatedDate >= boatFilters.CreatedDaysRange.InitialDate);

            if (boatFilters.CreatedDaysRange?.EndDate != null)
                boats = boats.Where(x => x.CreatedDate < boatFilters.CreatedDaysRange.EndDate);

            if (boatFilters.ExclusiveBoatId?.Count > 0)
                boats = boats.Where(x => !boatFilters.ExclusiveBoatId.Contains(x.Id));

            if (boatFilters.BoatIdList?.Count > 0)
                boats = boats.Where(x => boatFilters.BoatIdList.Contains(x.Id));

            return boats;
        }

        public async Task<List<BoatEN>> GetBoatFilteredList(BoatFilters boatFilters)
        {
            IQueryable<BoatEN> boats = GetBoatFiltered(boatFilters);

            return await boats.ToListAsync();
        }

        public async Task<IList<BoatEN>> GetAll(BoatFilters filters = null,
            Pagination pagination = null,
            Func<IQueryable<BoatEN>, IOrderedQueryable<BoatEN>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<BoatEN> boats = GetBoatFiltered(filters);

            if (orderBy == null)
                orderBy = b => b.OrderBy(x => x.Id);

            return await Get(boats, orderBy,includeProperties,pagination);
        }
        #endregion
    }
}
