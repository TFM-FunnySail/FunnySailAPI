﻿using FunnySailAPI.ApplicationCore.Exceptions;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Filters;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using FunnySailAPI.ApplicationCore.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ActivityCEN : IActivityCEN
    {
        private readonly IActivityCAD _activityCAD;

        public ActivityCEN(IActivityCAD activityCAD)
        {
            _activityCAD = activityCAD;
        }

        public async Task<int> AddActivity(DateTime activityDate, string name, decimal price, string description)
        {
            ActivityEN dbActivity = await _activityCAD.AddAsync(new ActivityEN
            {
                ActivityDate = activityDate,
                Name = name,
                Price = price,
                Description = description
            });

            return dbActivity.Id;
        }

        public IActivityCAD GetActivityCAD()
        {
            return _activityCAD;
        }

    }
}
