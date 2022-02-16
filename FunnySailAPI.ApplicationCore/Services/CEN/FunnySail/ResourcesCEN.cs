using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ResourcesCEN : IResourcesCEN
    {
        private readonly IResourcesCAD _resourcesCAD;
        public ResourcesCEN(IResourcesCAD resourcesCAD)
        {
            _resourcesCAD = resourcesCAD;
        }

        public async Task<int> AddResources(bool main, ResourcesEnum type,string uri)
        {
           ResourcesEN dbResources =  await _resourcesCAD.AddAsync(new ResourcesEN { 
                Main = main,
                Type = type,
                Uri = uri
           });

            return dbResources.Id;
        }
    }
}
