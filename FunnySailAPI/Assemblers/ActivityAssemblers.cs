using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.Assemblers
{
    public static class ActivityAssemblers
    {
        public static ActivityOutputDTO Convert(ActivityEN activityEN)
        {
            ActivityOutputDTO activityOutput = new ActivityOutputDTO
            {
                Id = activityEN.Id, 
                Active = activityEN.Active,
                Description = activityEN.Description,
                Name = activityEN.Name,
                Price = Math.Round(activityEN.Price, 2)
            };

            if(activityEN.ActivityResources != null)
            {
                activityOutput.ActivityResources = activityEN.ActivityResources.Select(x => new ActivityResourcesOutputDTO
                {
                    Uri = x.Resource.Uri,
                    Main = x.Resource.Main,
                    Type = x.Resource.Type
                }).ToList();
            }

            return activityOutput;
        }
    }
}
