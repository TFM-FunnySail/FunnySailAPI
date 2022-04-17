using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class ResourcesCEN : IResourcesCEN
    {
        private readonly IResourcesCAD _resourcesCAD;
        private readonly IWebHostEnvironment _environment;
        public ResourcesCEN(IResourcesCAD resourcesCAD,
                      IWebHostEnvironment environment)
        {
            _resourcesCAD = resourcesCAD;
            _environment = environment;
        }

        public async Task<int> AddResources(bool main, ResourcesEnum type,string uri)
        {
           ResourcesEN dbResources =  await _resourcesCAD.AddAsync(new ResourcesEN { 
                Main = main,
                Type = type,
                Uri = uri,
           });

            return dbResources.Id;
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            string uri = $"{Guid.NewGuid()}.jpg";
            //Subir imagen
            string path = Path.Combine(_environment.ContentRootPath, "wwwroot/Images", uri);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return uri;
        }

        public async Task DeleteResource(ResourcesEN resource)
        {
            string uri = resource.Uri;
            ResourcesEnum resourceType = resource.Type;

            await _resourcesCAD.Delete(resource);

            if(resourceType == ResourcesEnum.Image)
                RemovePhisicalImage(uri);
        }

        private static void RemovePhisicalImage(string uri)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", uri);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
