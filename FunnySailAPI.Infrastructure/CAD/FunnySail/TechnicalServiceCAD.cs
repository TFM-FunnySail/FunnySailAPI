﻿using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class TechnicalServiceCAD : BaseCAD<TechnicalServiceEN>, ITechnicalServiceCAD
    {
        public TechnicalServiceCAD(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TechnicalServiceEN> GetTechnicalServiceFiltered(TechnicalServiceFilters technicalServiceFilters)
        {
            IQueryable<TechnicalServiceEN> technicalServices = GetIQueryable();

            if (technicalServiceFilters == null)
                return technicalServices;
            if(technicalServiceFilters.Active != null)
                technicalServices = technicalServices.Where(x => x.Active == technicalServiceFilters.Active);
            if(technicalServiceFilters.Description != "")
                technicalServices = technicalServices.Where(x => x.Description == technicalServiceFilters.Description);
            if(technicalServiceFilters.Id != null)
                technicalServices = technicalServices.Where(x => x.Id == technicalServiceFilters.Id);
            if (technicalServiceFilters.Price != null)
                technicalServices = technicalServices.Where(x => x.Price == technicalServiceFilters.Price);
            return technicalServices;
        }
    }
}
